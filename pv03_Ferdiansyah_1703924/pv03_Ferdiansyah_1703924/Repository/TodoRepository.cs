﻿using pv03_Ferdiansyah_1703924.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pv03_Ferdiansyah_1703924.Repository
{
    class TodoRepository
    {
        string cs = @"server=localhost;userid=root;database=provis_masterdetail;SslMode=none";

        MySqlConnection conn;
        MySqlDataReader rdr;

        public TodoRepository()
        {
            conn = null;
            rdr = null;
        }

        public List<Todo> getByNim(string nim)
        {
            List<Todo> listTodo = new List<Todo>();

            conn = new MySqlConnection(cs);
            conn.Open();

            string stm = "SELECT * FROM todo where NimMhs = '" + nim + "';";
            MySqlCommand cmd = new MySqlCommand(stm, conn);
            rdr = cmd.ExecuteReader();

            
            while (rdr.Read())
            {
                Todo temp = new Todo();
                temp.Id = rdr.GetInt32(0);
                temp.NimMhs = rdr.GetString(1);
                temp.Nama = rdr.GetString(2);
                temp.Kategori = rdr.GetString(3);

                listTodo.Add(temp);
            }

            conn.Close();
            return listTodo;
        }

       public void addTodo(Todo todo)
        {
            string cs = @"server=localhost;userid=root;database=provis_masterdetail;SslMode=none";

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO todo(NimMhs,Nama,Kategori) Values(@Nim,@Nama,@Kategori)";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Nim", todo.NimMhs);
                cmd.Parameters.AddWithValue("@Nama", todo.Nama);
                cmd.Parameters.AddWithValue("@Kategori", todo.Kategori);
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error : {0}", ex.ToString());
            }
            finally
            {
                if(conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void deleteTodo(Todo todo)
        {
            string cs = @"server=localhost;userid=root;database=provis_masterdetail;SslMode=none";

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText ="Delete from todo where Id= @Id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Id", todo.Id);
                cmd.ExecuteNonQuery();

            }

            catch (MySqlException ex)
            {
                Console.WriteLine("Error : {0}", ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public void updateTodo(Todo todo)
        {
            string cs = @"server=localhost;userid=root;database=provis_masterdetail;SslMode=none";

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Update todo set Nama=@Nama,Kategori=@Kategori where Id=@Id";
                cmd.Prepare();

                cmd.Parameters.AddWithValue("@Id", todo.Id);
                cmd.Parameters.AddWithValue("@Nama", todo.Nama);
                cmd.Parameters.AddWithValue("@Kategori", todo.Kategori);
                cmd.ExecuteNonQuery();

            }

            catch (MySqlException ex)
            {
                Console.WriteLine("Error : {0}", ex.ToString());
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        public int countRows(string nim)
        {
            int total = 0;
            string cs = @"server=localhost;userid=root;database=provis_masterdetail;SslMode=none";

            MySqlConnection conn = null;
            conn = new MySqlConnection(cs);
            conn.Open();

            //command untuk menghitung jumlah record dengan nim yang dicari di tabel todo dan mahasiswas
            MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM todo WHERE NimMhs = " + nim, conn);
            MySqlCommand cmd1 = new MySqlCommand("SELECT COUNT(*) FROM mahasiswas WHERE Nim = " + nim, conn);

            //menjumlahkan jumlah record dari tabel todo dan mahasiswas
            total= Convert.ToInt32(cmd.ExecuteScalar())+ Convert.ToInt32(cmd1.ExecuteScalar());

            return total;
        }
    }
}
