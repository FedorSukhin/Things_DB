using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Media;

namespace Things_DB.Model
{
    internal class DBthingsClient
    {
        private ConnectionThingsDB connectionThingsDB;
        public DBthingsClient(ConnectionThingsDB connectionThingsDB)
        {
            this.connectionThingsDB = connectionThingsDB;
        }
        // ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ

        // 1. конвертация строки результата в объект FruitsVegetable
        private Things ConvertReaderRow(SqlDataReader reader)
        {
            int id = reader.GetInt32(reader.GetOrdinal("id"));
            string name = reader.GetString(reader.GetOrdinal("Name"));
            string type = reader.GetString(reader.GetOrdinal("type"));
            string supplyer = reader.GetString(reader.GetOrdinal("Supplyer"));
            int count = reader.GetInt32(reader.GetOrdinal("Count"));
            int cost = reader.GetInt32(reader.GetOrdinal("Cost"));
            DateTime date = reader.GetDateTime(reader.GetOrdinal("Date"));

            return new Things() { Id = id, Name = name, Type = type, 
                Supplyer = supplyer, Count = count, Cost = cost,Date =  date};

        }
        // 2. чтение табличного результата в список обектов
        private List<Things> ReadSelectResult(SqlDataReader reader)
        {
            List<Things> things = new List<Things>();
            while (reader.Read())
            {
                Things thing = ConvertReaderRow(reader);
                things.Add(thing);
            }
            return things;
        }
        // для объекта тип
        private NameThigs ConverterReaderRowType(SqlDataReader reader)
        {
        return new NameThigs() { Name = reader.GetString(reader.GetOrdinal("Type_f")) };
        }

        private List<NameThigs> ReadSelectResultType(SqlDataReader reader)
        {
            List<NameThigs> things = new List<NameThigs>();
            while (reader.Read())
            {
                NameThigs thing = ConverterReaderRowType(reader);
                things.Add(thing);
            }
            return things;
        }
        // для объекта производитель

        private NameThigs ConverterReaderRowSuplier(SqlDataReader reader)
        {
            return new NameThigs() { Name = reader.GetString(reader.GetOrdinal("SupplerName")) };
        }

        private List<NameThigs> ReadSelectResultSuplier(SqlDataReader reader)
        {
            List<NameThigs> things = new List<NameThigs>();
            while (reader.Read())
            {
                NameThigs thing = ConverterReaderRowSuplier(reader);
                things.Add(thing);
            }
            return things;
        }


        // 1. получить все записи
        public List<Things> SelectAll()
        {
            using (SqlConnection connection = connectionThingsDB.OpenDBConnection())
            {
                SqlCommand cmd = new SqlCommand("select th.id as ID, Name_f as Name,ty.Type_f as Type,s.SupplerName as Supplyer,\r\n" +
                    "SelfCost_f as Cost, Count_f as Count, Date_f as Date\r\nfrom Things_t th \r\n" +
                    "join Supplyer_t s on th.SupplerID_f=s.id\r\njoin Type_t ty on th.TypeID_f=ty.id;", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {                   
                    return ReadSelectResult(reader);
                }
            }
        }
        // Отображение всех типов товаров
        public List<NameThigs> TypeAll()
        {
            using (SqlConnection connection = connectionThingsDB.OpenDBConnection())
            {
                SqlCommand cmd = new SqlCommand("select Type_f from Type_t;", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return ReadSelectResultType(reader);
                }
            }
        }
        // отображение всех поставщиков
        public List<NameThigs> SupplierAll()
        {
            using (SqlConnection connection = connectionThingsDB.OpenDBConnection())
            {
                SqlCommand cmd = new SqlCommand("select SupplerName from  Supplyer_t;", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return ReadSelectResultSuplier(reader);
                }
            }
        }
        // товар с максимальным колличеством
        public List<Things> maxCount()
        {
            using (SqlConnection connection = connectionThingsDB.OpenDBConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "select th.id as ID, Name_f as Name,ty.Type_f as Type,s.SupplerName as Supplyer, " +
                    "SelfCost_f as Cost, Count_f as Count, Date_f as Date" +
                    " from Things_t th " +
                    "join Supplyer_t s on th.SupplerID_f=s.id " +
                    "join Type_t ty on th.TypeID_f=ty.id " +
                    "where th.Count_f like (select max(Count_f) from Things_t );", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return ReadSelectResult(reader);
                }
            }
        }
        // товар с минимальным колличеством
        public List<Things> minCount()
        {
            using (SqlConnection connection = connectionThingsDB.OpenDBConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "select th.id as ID, Name_f as Name,ty.Type_f as Type,s.SupplerName as Supplyer, " +
                    "SelfCost_f as Cost, Count_f as Count, Date_f as Date" +
                    " from Things_t th " +
                    "join Supplyer_t s on th.SupplerID_f=s.id " +
                    "join Type_t ty on th.TypeID_f=ty.id " +
                    "where th.Count_f like (select min(Count_f) from Things_t );", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return ReadSelectResult(reader);
                }
            }
        }
        // товар с максимальной себестоимостью
        public List<Things> maxCost()
        {
            using (SqlConnection connection = connectionThingsDB.OpenDBConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "select th.id as ID, Name_f as Name,ty.Type_f as Type,s.SupplerName as Supplyer, " +
                    "SelfCost_f as Cost, Count_f as Count, Date_f as Date" +
                    " from Things_t th " +
                    "join Supplyer_t s on th.SupplerID_f=s.id " +
                    "join Type_t ty on th.TypeID_f=ty.id " +
                    "where th.SelfCost_f like (select max(SelfCost_f) from Things_t );", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return ReadSelectResult(reader);
                }
            }
        }
        // товар с минимальной себестоимостью
        public List<Things> minCost()
        {
            using (SqlConnection connection = connectionThingsDB.OpenDBConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "select th.id as ID, Name_f as Name,ty.Type_f as Type,s.SupplerName as Supplyer, " +
                    "SelfCost_f as Cost, Count_f as Count, Date_f as Date" +
                    " from Things_t th " +
                    "join Supplyer_t s on th.SupplerID_f=s.id " +
                    "join Type_t ty on th.TypeID_f=ty.id " +
                    "where th.SelfCost_f like (select min(SelfCost_f) from Things_t );", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return ReadSelectResult(reader);
                }
            }
        }
        //товары определенного типа
        public List<Things> ChoisType(string type)
        {
            using (SqlConnection connection = connectionThingsDB.OpenDBConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "select th.id as ID, Name_f as Name,ty.Type_f as Type,s.SupplerName as Supplyer, " +
                    "SelfCost_f as Cost, Count_f as Count, Date_f as Date" +
                    " from Things_t th " +
                    "join Supplyer_t s on th.SupplerID_f=s.id " +
                    "join Type_t ty on th.TypeID_f=ty.id " +
                    "where ty.Type_f like @type;", connection);
                cmd.Parameters.Add("@type", System.Data.SqlDbType.NVarChar).Value = type;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return ReadSelectResult(reader);
                }
            }
        }
        //товары определенного производителя
        public List<Things> ChoisSupplier(string suppler)
        {
            using (SqlConnection connection = connectionThingsDB.OpenDBConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "select th.id as ID, Name_f as Name,ty.Type_f as Type,s.SupplerName as Supplyer, " +
                    "SelfCost_f as Cost, Count_f as Count, Date_f as Date" +
                    " from Things_t th " +
                    "join Supplyer_t s on th.SupplerID_f=s.id " +
                    "join Type_t ty on th.TypeID_f=ty.id " +
                    "where s.SupplerName like @suppler;", connection);
                cmd.Parameters.Add("@suppler", System.Data.SqlDbType.NVarChar).Value = suppler;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    return ReadSelectResult(reader);
                }
            }
        }
    }
}
