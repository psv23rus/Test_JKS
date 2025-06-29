using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Test_JKS
{
    class SqlQuery_ItemInStore
    {

        private readonly string MyConn = Properties.Settings.Default.connsql;
        public async Task<DataTable> GetItemInStore(int Item_id)
        {
            //создать datatable
            DataTable ItemInStore = new DataTable("GetItemInStore");
            //подключиться к sql server
            try
            {
                using (SqlConnection connection = new SqlConnection(MyConn))
                {

                    //создать команду для хранимой процедуры
                    using (SqlCommand command_GetItemInStore = new SqlCommand("Proc_GetItemInStore", connection))
                    {
                        command_GetItemInStore.CommandType = CommandType.StoredProcedure;

 /*                       // определяем первый выходной параметр
                        SqlParameter messageParam = new SqlParameter
                        {
                            ParameterName = "@message",
                            SqlDbType = SqlDbType.VarChar // тип параметра
                        };
                        // указываем, что параметр будет выходным
                        messageParam.Direction = ParameterDirection.Output;
                        command_GetItemInStore.Parameters.Add(messageParam);
                        string message = command_GetItemInStore.Parameters["@message"].Value.ToString();
 */                       


                        // добавляем параметр
                        SqlParameter ItemsID_Param = new SqlParameter
                        {
                            ParameterName = "@Item_id",
                            Value = Item_id
                        };
                        command_GetItemInStore.Parameters.Add(ItemsID_Param);



                        //подключаемся
                        await connection.OpenAsync();
                        //получаем
                        using (var result = await command_GetItemInStore.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            ItemInStore.Load(result);
                        }
                    }
                }
            }
            catch (Exception exeption)
            {
                throw; }

            return ItemInStore;
        }

    
    }
}
