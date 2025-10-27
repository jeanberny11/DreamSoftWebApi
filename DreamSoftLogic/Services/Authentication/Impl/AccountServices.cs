using System.Security.Cryptography;
using AutoMapper;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Entities.Generics;
using DreamSoftData.Repositories.Authentication.Interfaces;
using DreamSoftData.Repositories.Generics.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Authentication.Interfaces;
using DreamSoftModel.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DreamSoftLogic.Services.Authentication.Impl;

public class AccountServices(IAccountRepository repository, IMapper mapper,IUsersRepository usersRepository,IDefaultValSetupsRepository defaultValSetupsRepository,IPasswordHasher<Users> passwordHasher)
    : GenericServices<Accounts, Account, int>(repository, mapper), IAccountServices
{
    private readonly IMapper _mapper = mapper;

    public async Task<Account> CreateNewAccount(AccountCreate account)
    {
        var defaultVal = await defaultValSetupsRepository.GetByIdAsync(1);
        if (defaultVal == null)
        {
            throw new Exception("Default values setup not found.");
        }
        var transaction = await repository.GetTransaction();
        try
        {
            if (await repository.CheckEmailExistence(account.Email))
            {
                throw new Exception("Ya existe una cuenta con ese correo electronico!");
            }

            if (await usersRepository.IsUserExists(account.UserName))
            {
                throw new Exception("Ya existe un usuario con ese nombre de usuario!");
            }
            var accountNumber = GenerateAccountNumber(account.UserName, account.Password);
            account.AccountNumber = accountNumber.ToString("D20"); // Ensure it's 20 digits long
            var accountEntity = new Accounts()
            {
                FirstName = account.FirstName,
                LastName = account.LastName,
                Phone = account.Phone,
                Email = account.Email,
                Address = account.Address,
                CountryId = account.Country.CountryId,
                ProvinceId = account.Province.ProvinceId,
                MunicipalityId = account.Municipality.MunicipalityId,
                AccountTypeId = account.AccountType.AccountTypeId,
                Dob = account.Dob,
                GenderId = account.Gender.GenderId,
                Active = true,
                AccountNumber = account.AccountNumber,
                CDate = DateTime.UtcNow,
                IdNumber = account.IdNumber,
                IdTypeId = account.IdType.IdTypeId
            };
            var accountResult = await repository.CreateAsync(accountEntity);
            if (accountResult == null)
            {
                throw new Exception("Error creating account in the database");
            }
            await repository.SaveChangesAsync();

            var userEntity = new Users()
            {
                AccountId = accountResult.AccountId,
                FirstName = account.FirstName,
                LastName = account.LastName,
                UserName = account.UserName,
                RoleId = defaultVal.NewAccountRoleId,
                Active = true
            };
            userEntity.Password = passwordHasher.HashPassword(userEntity, account.Password);

            var userResult = await usersRepository.CreateAsync(userEntity);
            if (userResult == null)
            {
                throw new Exception("Error creating user in the database");
            }
            await usersRepository.SaveChangesAsync();
            await transaction.CommitAsync();

            return _mapper.Map<Account>(accountResult);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private int GenerateAccountNumber(string username,string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(username + password));

        // Take the first 4 bytes and convert to int
        int number = BitConverter.ToInt32(bytes, 0);

        // Optional: Make sure it's non-negative
        return Math.Abs(number);
    }
}
