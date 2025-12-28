##❗Important rule (this is the core lesson)

Never construct response DTOs that depend on DB-generated values before SaveChanges()
this applies to:

- IDs
- timestamps
- computed columns
- triggers
- version columns

Below Image causing returned user.Id = 0 as havent done commit async for transaction 
<img width="707" height="491" alt="image" src="https://github.com/user-attachments/assets/ec6c0036-83e9-473b-9c53-563861c6a240" />
