using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacultyAppWeb.RepositoryServices.MySQL
{
    public class MySqlProfessorRepository : IProfessorRepository
    {
        public string TableName { get; set; } = "professors";
        public MySqlConnection Connection { get; set; }
        public MySqlTransaction Transaction { get; set; }
        public Professor Add(Professor professor)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"INSERT INTO {TableName} (FirstName, LastName, JMBG) VALUES ('{professor.FirstName}','{professor.LastName}','{professor.JMBG}')";
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;
                professor.Id = id;

                Transaction.Commit();

                return professor;
            }
            catch (Exception ex)
            {
                if (Connection != null)
                    Transaction?.Rollback();
                throw ex;
            }
            finally
            {
                Connection?.Close();
            }
        }

        public Professor Delete(long id)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"DELETE FROM {TableName} WHERE Id = {id}";
                cmd.ExecuteNonQuery();


                Transaction.Commit();

                return null;
            }
            catch (Exception ex)
            {
                if (Connection != null)
                {
                    Transaction?.Rollback();
                }
                throw ex;
            }
            finally
            {
                Connection?.Close();
            }
        }

        public Professor GetById(long id)
        {
            try
            {

                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT Id,FirstName,LastName,JMBG FROM {TableName} WHERE Id = {id}";

                dataReader = cmd.ExecuteReader();

                Professor professor = null;

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    professor = new Professor
                    {
                        Id = (long)dataReader.GetUInt64("Id"),
                        FirstName = dataReader.GetString("FirstName"),
                        LastName = dataReader.GetString("LastName"),
                        JMBG = dataReader.GetString("JMBG")
                    };
                }

                dataReader.Close();
                Transaction.Commit();

                return professor;
            }
            catch (Exception ex)
            {
                if (Connection != null)
                    Transaction?.Rollback();
                throw ex;
            }
            finally
            {
                Connection?.Close();
            }
        }

        public Professor GetByJMBG(string JMBG)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT Id,FirstName,LastName,JMBG FROM {TableName} WHERE JMBG = {JMBG}";

                dataReader = cmd.ExecuteReader();

                Professor professor = null;

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    professor = new Professor
                    {
                        Id = (long)dataReader.GetUInt64("Id"),
                        FirstName = dataReader.GetString("FirstName"),
                        LastName = dataReader.GetString("LastName"),
                        JMBG = dataReader.GetString("JMBG")
                    };

                }

                dataReader.Close();

                Transaction.Commit();

                return professor;
            }
            catch (Exception ex)
            {
                if (Connection != null)
                    Transaction?.Rollback();
                throw ex;
            }
            finally
            {
                Connection?.Close();
            }
        }

        public IEnumerable<Professor> GetProfessorsByName(string name)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT Id,FirstName,LastName,JMBG FROM {TableName} WHERE FirstName LIKE '" + name + "%'";

                dataReader = cmd.ExecuteReader();

                List<Professor> professors= new List<Professor>();

                while (dataReader.Read())
                {
                    Professor professor = new Professor
                    {
                        Id = (long)dataReader.GetUInt64("Id"),
                        FirstName = dataReader.GetString("FirstName"),
                        LastName = dataReader.GetString("LastName"),
                        JMBG = dataReader.GetString("JMBG")
                    };

                    professors.Add(professor);

                }

                dataReader.Close();
                Transaction.Commit();

                return professors;
            }
            catch (Exception ex)
            {
                if (Connection != null)
                    Transaction?.Rollback();
                throw ex;
            }
            finally
            {
                Connection?.Close();
            }
        }

        public Professor Update(Professor updated)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"UPDATE {TableName} SET FirstName = '{updated.FirstName}', LastName = '{updated.LastName}' WHERE Id = {updated.Id}";

                cmd.ExecuteNonQuery();

                Transaction.Commit();

                return updated;
            }
            catch (Exception ex)
            {
                if (Connection != null)
                    Transaction?.Rollback();
                throw ex;
            }
            finally
            {
                Connection?.Close();
            }
        }
    }
}
