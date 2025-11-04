using System.Security.Cryptography;
using AutoMapper;
using DreamSoftData.Entities.Authentication;
using DreamSoftData.Repositories.Authentication.Interfaces;
using DreamSoftData.Repositories.Generics.Interfaces;
using DreamSoftLogic.Services.Base;
using DreamSoftLogic.Services.Authentication.Interfaces;
using DreamSoftModel.Models.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DreamSoftLogic.Services.Authentication.Impl;

public class AccountServices(IAccountRepository repository, IMapper mapper,IUsersRepository usersRepository,IDefaultValSetupsRepository defaultValSetupsRepository,IPasswordHasher<Users> passwordHasher)
    : ActiveGenericServices<Accounts, Account, int>(repository, mapper), IAccountServices
{
    private readonly IMapper _mapper = mapper;

    public async Task<Account> CreateNewAccount(AccountCreate account)
    {
        var defaultVal = await defaultValSetupsRepository.GetByIdAsync(1) ?? throw new Exception("Algunos valores predeterminados no estan configurados en el sistema. Por favor contacte al administrador.");
        var transaction = await repository.GetTransaction();
        try
        {
            if (await repository.CheckEmailExistence(account.Email))
            {
                throw new Exception("Ya existe una cuenta con ese correo electronico!");
            }

            if (await usersRepository.IsUserExists(account.Username))
            {
                throw new Exception("Ya existe un usuario con ese nombre de usuario!");
            }
            var accountNumber = GenerateAccountNumber(account.Username, account.Password).ToString("D20");
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
                AccountTypeId = defaultVal.DefaultAccountTypeId,
                Dob = account.Dob,
                GenderId = account.Gender.GenderId,
                Active = true,
                AccountNumber = accountNumber,
                CDate = DateTime.UtcNow,
                IdNumber = account.IdNumber,
                IdTypeId = account.IdType.IdTypeId,
                EmailVerified = account.EmailVerified,
            };
            var accountResult = await repository.CreateAsync(accountEntity) ?? throw new Exception("Ha ocurrido un error al crear la cuenta en la base de datos");
            await repository.SaveChangesAsync();

            var userEntity = new Users()
            {
                AccountId = accountResult.AccountId,
                UserName = account.Username,
                RoleId = defaultVal.NewAccountRoleId,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Active = true
            };
            userEntity.Password = passwordHasher.HashPassword(userEntity, account.Password);

            var userResult = await usersRepository.CreateAsync(userEntity) ?? throw new Exception("Ha ocurrido un error al crear el usuario en la base de datos");
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

    public Task<bool> CheckEmailExistence(string email)
    {
        return repository.CheckEmailExistence(email);
    }

    private static int GenerateAccountNumber(string username,string password)
    {
        var bytes = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(username + password));

        // Take the first 4 bytes and convert to int
        int number = BitConverter.ToInt32(bytes, 0);

        // Optional: Make sure it's non-negative
        return Math.Abs(number);
    }
}
