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
    public class MySqlStudentRepository : IStudentRepository
    {

        public String TableName { get; set; } = "Students";
        public MySqlConnection Connection { get; set; }
        public MySqlTransaction Transaction { get; set; }

        public Student Add(Student student)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"INSERT INTO {TableName} (Indeks, FirstName, LastName, JMBG) VALUES ('{student.Index}','{student.FirstName}','{student.LastName}','{student.JMBG}');";
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;
                student.Id = id;

                Transaction.Commit();

                return student;
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

        public Student Delete(long id)
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

        public Student GetById(long id)
        {
            try
            {

                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT Id,FirstName,LastName,Indeks,JMBG FROM {TableName} WHERE Id = {id}";

                dataReader = cmd.ExecuteReader();

                Student student = null;

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    student = new Student();
                    student.Id = (long)dataReader.GetUInt64("Id");
                    student.Index = dataReader.GetString("Indeks");
                    student.FirstName = dataReader.GetString("FirstName");
                    student.LastName = dataReader.GetString("LastName");
                    student.JMBG = dataReader.GetString("JMBG");
                }

                dataReader.Close();
                Transaction.Commit();

                return student;
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

        public IEnumerable<Student> GetStudentsByIndex(string index)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT Id,FirstName,LastName,Indeks,JMBG FROM {TableName} WHERE Indeks LIKE '" + index + "%'";

                dataReader = cmd.ExecuteReader();

                List<Student> students = new List<Student>();

                while (dataReader.Read())
                {
                    long id = (long)dataReader.GetUInt64("Id");
                    string indeks = dataReader.GetString("Indeks");
                    string firstName = dataReader.GetString("FirstName");
                    string lastName = dataReader.GetString("LastName");
                    string jmbg = dataReader.GetString("JMBG");

                    //Student student = new Student(id, firstName, lastName, indeks, jmbg, "");
                   // students.Add(student);

                }

                dataReader.Close();
                Transaction.Commit();

                return students;
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

        public Student GetStudentByJMBG(string JMBG)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT Id,FirstName,LastName,Indeks,JMBG FROM {TableName} WHERE JMBG = {JMBG}";

                dataReader = cmd.ExecuteReader();

                Student student = null;

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    student = new Student();
                    student.Id = (long)dataReader.GetUInt64("Id");
                    student.Index = dataReader.GetString("Indeks");
                    student.FirstName = dataReader.GetString("FirstName");
                    student.LastName = dataReader.GetString("LastName");
                    student.JMBG = dataReader.GetString("JMBG");
                    
                }

                dataReader.Close();

                Transaction.Commit();

                return student;
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

        public Student Update(Student updated)
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

        public IEnumerable<Student> GetStudentsByIndex(StudentParameters param, string index)
        {
            throw new NotImplementedException();
        }

        PagedList<Student> IStudentRepository.GetStudentsByIndex(StudentParameters param, string index)
        {
            throw new NotImplementedException();
        }

        public int GetStudentsNumber(string index)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExamRegistration> GetFailedExams(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExamRegistration> GetPassedExams(long id)
        {
            throw new NotImplementedException();
        }
    }
}
