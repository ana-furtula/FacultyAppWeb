using FacultyAppWeb.Domains;
using FacultyAppWeb.RepositoryServices.Interfaces;
using MySql.Data.MySqlClient;

namespace FacultyAppWeb.RepositoryServices.MySQL
{
    public class MySqlSubjectRepository : ISubjectRepository
    {
        public string TableName { get; set; } = "subjects";
        public MySqlConnection Connection { get; set; }
        public MySqlTransaction Transaction { get; set; }

        public Subject Add(Subject subject)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"INSERT INTO {TableName} (Name, ESPB, Semester) VALUES ('{subject.Name}', {subject.ESPB},{subject.Semester})";
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;
                subject.Id = id;

                Transaction.Commit();

                return subject;
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

        public Subject Delete(long id)
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

        public bool Exists(Subject subject)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT count(*) as count FROM {TableName} WHERE Semester = {subject.Semester} AND ESPB = {subject.ESPB} AND Name = '{subject.Name}'";

                dataReader = cmd.ExecuteReader();
                dataReader.Read();
                int count = dataReader.GetInt16("count");

                dataReader.Close();

                Transaction.Commit();

                return count > 0 ? true : false;
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

        public Subject GetById(long id)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT Id,Name,ESPB,Semester FROM {TableName} WHERE Id = {id}";

                dataReader = cmd.ExecuteReader();

                Subject subject = null;

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    subject = new Subject
                    {
                        Id = (long)dataReader.GetUInt64("Id"),
                        Name = dataReader.GetString("Name"),
                        ESPB = dataReader.GetInt16("ESPB"),
                        Semester = dataReader.GetInt16("Semester")
                    };
                }

                dataReader.Close();
                Transaction.Commit();

                return subject;
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

        public PagedList<Subject> GetSubjectsByName(SubjectParameters param, string index)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Subject> GetSubjectsByESPB(int espb)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT Id,Name,Semester,ESPB FROM {TableName} WHERE ESPB = {espb}";

                dataReader = cmd.ExecuteReader();

                List<Subject> subjects = new List<Subject>();
                while (dataReader.Read())
                {
                    Subject subject = new Subject
                    {
                        Id = (long)dataReader.GetUInt64("Id"),
                        Name = dataReader.GetString("Name"),
                        Semester = dataReader.GetInt16("Semester"),
                        ESPB = dataReader.GetInt16("ESPB")
                    };
                    subjects.Add(subject);
                }

                dataReader.Close();

                Transaction.Commit();

                return subjects;
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

        public IEnumerable<Subject> GetSubjectsByName(string name)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT Id,Name,Semester,ESPB FROM {TableName} WHERE Name LIKE '{name}%'";

                dataReader = cmd.ExecuteReader();

                List<Subject> subjects = new List<Subject>();
                while (dataReader.Read())
                {
                    Subject subject = new Subject
                    {
                        Id = (long)dataReader.GetUInt64("Id"),
                        Name = dataReader.GetString("Name"),
                        Semester = dataReader.GetInt16("Semester"),
                        ESPB = dataReader.GetInt16("ESPB")
                    };
                    subjects.Add(subject);
                }

                dataReader.Close();

                Transaction.Commit();

                return subjects;
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

        public IEnumerable<Subject> GetSubjectsBySemester(int semester)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT Id,Name,Semester,ESPB FROM {TableName} WHERE Semester = {semester}";

                dataReader = cmd.ExecuteReader();

                List<Subject> subjects = new List<Subject>();
                while (dataReader.Read())
                {
                    Subject subject = new Subject
                    {
                        Id = (long)dataReader.GetUInt64("Id"),
                        Name = dataReader.GetString("Name"),
                        Semester = dataReader.GetInt16("Semester"),
                        ESPB = dataReader.GetInt16("ESPB")
                    };
                    subjects.Add(subject);
                }

                dataReader.Close();

                Transaction.Commit();

                return subjects;
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

        public int GetTotalSubjectNumber(string index)
        {
            throw new NotImplementedException();
        }

        public Subject Update(Subject updated)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"UPDATE {TableName} SET Name = '{updated.Name}', ESPB = {updated.ESPB}, Semester = {updated.Semester} WHERE Id = {updated.Id}";

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
