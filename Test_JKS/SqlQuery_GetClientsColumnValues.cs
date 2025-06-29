using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Test_JKS
{
    public class SqlQuery_GetClientsColumnValues
    {
        private readonly string MyConn = Properties.Settings.Default.connsql;
        public async Task<DataTable> GetColumnValues(string columnName)
        {
            //создать datatable
            DataTable GetColumnValue = new DataTable("GetColumnValue");
            //подключиться к sql server
            try
            {
                using (SqlConnection connection = new SqlConnection(MyConn))
                {

                    //создать команду для хранимой процедуры
                    using (SqlCommand command_GetColumnValue = new SqlCommand("Proc_GetClientsColumnValues", connection))
                    {
                        command_GetColumnValue.CommandType = CommandType.StoredProcedure;
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
