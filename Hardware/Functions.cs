﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hardware
{
    class Functions
    {
        private SqlConnection Con;
        private SqlCommand Cmd;
        private DataTable dt;
        private string ConStr;
        private SqlDataAdapter Sda;

        public Functions()
          {
              ConStr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Dilshani\OneDrive\Documents\HardwareshopDb.mdf;Integrated Security=True;Connect Timeout=30";
              Con = new SqlConnection(ConStr);
              Cmd = new SqlCommand();
              Cmd.Connection = Con;  
           }

        public DataTable GetData(string Query)
        {
            dt = new DataTable();
            Sda= new SqlDataAdapter(Query,ConStr);
            Sda.Fill(dt);
            return dt;
        }

        public int SetData(String Query)
        {
            int Cnt = 0;
            if (Con.State == ConnectionState.Closed)
            { 
                Con.Open();
            }
            Cmd.CommandText= Query;
            Cnt = Cmd.ExecuteNonQuery();
            return Cnt;
        }

    }
}
