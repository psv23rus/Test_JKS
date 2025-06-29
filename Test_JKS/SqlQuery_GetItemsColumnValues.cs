using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Test_JKS
{
    public class SqlQuery_GetItemsColumnValues
    {
        private readonly string MyConn = Properties.Settings.Default.connsql;
        public async Task<DataTable> GetItemsColumnValues(string columnName)
        {
            //создать datatable
            DataTable GetColumnValue = new DataTable("GetColumnValue");
            //подключиться к sql server
            try
            {
                using (SqlConnection connection = new SqlConnection(MyConn))
                {

                    //создать команду для хранимой процедуры
                    using (SqlCommand command_GetColumnValue = new SqlCommand("Proc_GetItemsColumnValues", connection))
                    {
                        command_GetColumnValue.CommandType = CommandType.StoredProcedure;

/*                        SqlParameter messageParam = new SqlParameter
                        {
                            ParameterName = "@message",
                            SqlDbType = SqlDbType.VarChar // тип параметра
                        };
                        // указываем, что параметр будет выходным
                        messageParam.Direction = ParameterDirection.Output;
                        command_GetColumnValue.Parameters.Add(messageParam);
                        string message = command_GetColumnValue.Parameters["@message"].Value.ToString();
*/

                        // добавляем параметр
                        SqlParameter ColumnName_Param = new SqlParameter
                        {
                            ParameterName = "@ColumnName",
                            Value = columnName
                        };
                        command_GetColumnValue.Parameters.Add(ColumnName_Param);
                        //подключаемся
                        await connection.OpenAsync();
                        //получаем
                        using (var result = await command_GetColumnValue.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            GetColumnValue.Load(result);
                        }
                    }
                }
            }
            catch (Exception exeption)
            { throw; }

            return GetColumnValue;
        }
    }
}
