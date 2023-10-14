insert into Things_t (Name_f, Count_f, SelfCost_f,Date_f,TypeID_f,SupplerID_f)
values('€блоко',10,30,'20.04.2023',3,3)

insert into Sypplyer_t(SupplerName) values ('акрек')

insert into Type_t(Type_f) values ('прочее')

select th.id as ID, Name_f as Name,ty.Type_f as Type,s.SupplerName as Supplyer,
SelfCost_f as Cost, Count_f as Count, Date_f as Date

from Things_t th 
join Sypplyer_t s on th.SupplerID_f=s.id
join Type_t ty on th.TypeID_f=ty.id

select * from Sypplyer_t
select * from Type_t