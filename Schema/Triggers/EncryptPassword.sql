CREATE TRIGGER encrypt_password
ON Users
INSTEAD OF INSERT
AS
BEGIN
    INSERT INTO Users(Name, Number, Mail, Password, GSTIN, Created_at)
    SELECT inserted.Name, inserted.Number, inserted.Mail, HASHBYTES('MD5', inserted.Password), inserted.GSTIN, inserted.Created_at FROM inserted;
END;