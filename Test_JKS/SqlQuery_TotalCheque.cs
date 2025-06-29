using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;


namespace Test_JKS
{
   public class SqlQuery_TotalCheque
    {
        //создать переменную для строки подключения
        private readonly string MyConn = Properties.Settings.Default.connsql;
         public async Task<DataTable> GetCheque(int Client_id)
        {
            //создать datatable
            DataTable Total_Cheque = new DataTable("GetCheque");
            //подключиться к sql server
            try
            {
                using (SqlConnection connection = new SqlConnection(MyConn))
                {

                    //создать команду для хранимой процедуры
                    using (SqlCommand command_GetTotalCheque = new SqlCommand("Proc_Total_cheque", connection))
                    {
                        command_GetTotalCheque.CommandType = CommandType.StoredProcedure;
                        // добавляем параметр
                        SqlParameter ClientID_Param = new SqlParameter
                        {
                            ParameterName = "@Client_id",
                            Value = Client_id
                        };
                        command_GetTotalCheque.Parameters.Add(ClientID_Param);

                        //подключаемся
                        await connection.OpenAsync();
                        //получаем
                        using (var result = await command_GetTotalCheque.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                        {
                            Total_Cheque.Load(result);
                        }
                    }
                }
            }
            catch (Exception exeption)
            { throw; }

            return Total_Cheque;
        }

    }
}
