-- Create the passwordresettokens table
CREATE TABLE IF NOT EXISTS passwordresettokens (
    passwordresettokenid SERIAL PRIMARY KEY,
    tokenhash VARCHAR(256) NOT NULL,
    userid INTEGER NOT NULL,
    accountid INTEGER NOT NULL,
    email VARCHAR(255) NOT NULL,
    username VARCHAR(50) NOT NULL,
    expiresat TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    isused BOOLEAN NOT NULL DEFAULT FALSE,
    createdat TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT (NOW() AT TIME ZONE 'UTC'),

    CONSTRAINT fk_passwordresettokens_users
        FOREIGN KEY (userid) REFERENCES users(userid) ON DELETE CASCADE,
    CONSTRAINT fk_passwordresettokens_accounts
        FOREIGN KEY (accountid) REFERENCES accounts(accountid) ON DELETE CASCADE
);

-- Create indexes for performance
CREATE INDEX IF NOT EXISTS ix_passwordresettokens_tokenhash
    ON passwordresettokens(tokenhash);

CREATE INDEX IF NOT EXISTS ix_passwordresettokens_userid
    ON passwordresettokens(userid);

CREATE INDEX IF NOT EXISTS ix_passwordresettokens_accountid
    ON passwordresettokens(accountid);

CREATE INDEX IF NOT EXISTS ix_passwordresettokens_email_username
    ON passwordresettokens(email, username);

CREATE INDEX IF NOT EXISTS ix_passwordresettokens_expiresat
    ON passwordresettokens(expiresat);

CREATE INDEX IF NOT EXISTS ix_passwordresettokens_createdat
    ON passwordresettokens(createdat);
