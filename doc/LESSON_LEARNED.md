# Got Id=0 when query entity

## ❗Important rule (this is the core lesson)

Never construct response DTOs that depend on DB-generated values before SaveChanges()
this applies to:

- IDs
- timestamps
- computed columns
- triggers
- version columns

The image below illustrates this problem: returned user.Id = 0 as havent done commit async for transaction
<img width="707" height="491" alt="image" src="https://github.com/user-attachments/assets/ec6c0036-83e9-473b-9c53-563861c6a240" />

### Solution

With API That need to returned the above information (DB-generated), please:

- always use raw entity (get from DB) to pass repo -> service -> use-case
- DTO mapping only happen in controller
For example:
<img width="907" height="285" alt="image" src="https://github.com/user-attachments/assets/f8dd47cd-a1df-4f09-8d71-1022a75857be" />


### one-sentence takeaway (memorize this)

Mutations return entities; controllers return DTOs.


# Return surplus nested entities
Never return EF entities from controllers. Ever.

not because EF is bad
but because entities are graphs, not messages