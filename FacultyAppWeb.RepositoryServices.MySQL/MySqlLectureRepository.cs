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
    public class MySqlLectureRepository : ILectureRepository
    {
        public string TableName { get; set; } = "professor_subject";
        public MySqlConnection Connection { get; set; }
        public MySqlTransaction Transaction { get; set; }

        public Lecture Add(Lecture lecture)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"INSERT INTO {TableName} (ProfessorId, SubjectId) VALUES ({lecture.Professor.Id}, {lecture.Subject.Id})";
                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;
                lecture.Id = id;

                Transaction.Commit();

                return lecture;
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

        public Lecture Delete(long id)
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

        public bool Exists(Lecture lecture)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT count(*) as count FROM {TableName} WHERE ProfessorId = {lecture.Professor.Id} AND SubjectId = {lecture.Subject.Id}";

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

        public Lecture GetById(long id)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT l.Id as lid,s.Id as sid, s.Name,s.ESPB,s.Semester, p.Id as pid, p.FirstName, p.LastName, p.JMBG FROM {TableName} l INNER JOIN subjects s ON (l.SubjectId = s.Id) INNER JOIN professors p ON (l.ProfessorId = p.Id) WHERE Id = {id}";

                dataReader = cmd.ExecuteReader();

                Lecture lecture = null;

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    Subject subject = new Subject
                    {
                        Id = (long)dataReader.GetUInt64("sid"),
                        Name = dataReader.GetString("Name"),
                        ESPB = dataReader.GetInt16("ESPB"),
                        Semester = dataReader.GetInt16("Semester")
                    };

                    Professor professor = new Professor
                    {
                        Id = (long)dataReader.GetUInt64("pid"),
                        FirstName = dataReader.GetString("FirstName"),
                        LastName = dataReader.GetString("LastName"),
                        JMBG = dataReader.GetString("JMBG")
                    };

                    lecture = new()
                    {
                        Id = dataReader.GetInt64("lid"),
                        Subject = subject,
                        Professor = professor
                    };
                }

                dataReader.Close();
                Transaction.Commit();

                return lecture;
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

        public IEnumerable<Lecture> GetLecturesByProfessorName(string professorName)
        {
            try{
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT l.Id as lid,s.Id as sid, s.Name,s.ESPB,s.Semester, p.Id as pid, p.FirstName, p.LastName, p.JMBG FROM {TableName} l INNER JOIN subjects s ON (l.SubjectId = s.Id) INNER JOIN professors p ON (l.ProfessorId = p.Id) WHERE p.FirstName LIKE '{professorName}%'";

                dataReader = cmd.ExecuteReader();

                List<Lecture> lectures = new();

                while (dataReader.Read())
                {
                    Subject subject = new()
                    {
                        Id = (long)dataReader.GetUInt64("sid"),
                        Name = dataReader.GetString("Name"),
                        ESPB = dataReader.GetInt16("ESPB"),
                        Semester = dataReader.GetInt16("Semester")
                    };

                    Professor professor = new()
                    {
                        Id = (long)dataReader.GetUInt64("pid"),
                        FirstName = dataReader.GetString("FirstName"),
                        LastName = dataReader.GetString("LastName"),
                        JMBG = dataReader.GetString("JMBG")
                    };

                    Lecture lecture = new()
                    {
                        Id = dataReader.GetInt64("lid"),
                        Subject = subject,
                        Professor = professor
                    };
                    lectures.Add(lecture);
                }

                dataReader.Close();
                Transaction.Commit();

                return lectures;
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

        public IEnumerable<Lecture> GetLecturesBySubjectName(string subjectName)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT l.Id as lid,s.Id as sid, s.Name,s.ESPB,s.Semester, p.Id as pid, p.FirstName, p.LastName, p.JMBG FROM {TableName} l INNER JOIN subjects s ON (l.SubjectId = s.Id) INNER JOIN professors p ON (l.ProfessorId = p.Id) WHERE s.Name LIKE '{subjectName}%'";

                dataReader = cmd.ExecuteReader();

                List<Lecture> lectures = new();

                while (dataReader.Read())
                {
                    Subject subject = new()
                    {
                        Id = (long)dataReader.GetUInt64("sid"),
                        Name = dataReader.GetString("Name"),
                        ESPB = dataReader.GetInt16("ESPB"),
                        Semester = dataReader.GetInt16("Semester")
                    };

                    Professor professor = new()
                    {
                        Id = (long)dataReader.GetUInt64("pid"),
                        FirstName = dataReader.GetString("FirstName"),
                        LastName = dataReader.GetString("LastName"),
                        JMBG = dataReader.GetString("JMBG")
                    };

                    Lecture lecture = new()
                    {
                        Id = dataReader.GetInt64("lid"),
                        Subject = subject,
                        Professor = professor
                    };
                    lectures.Add(lecture);
                }

                dataReader.Close();
                Transaction.Commit();

                return lectures;
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

        public IEnumerable<Professor> GetProfessorsForSubject(long subjectId)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT p.Id, p.FirstName, p.LastName, p.JMBG FROM {TableName} l INNER JOIN professors p ON (l.ProfessorId = p.Id) WHERE SubjectId = {subjectId}";

                dataReader = cmd.ExecuteReader();

                List<Professor> professors = new();

                while (dataReader.Read())
                {
                    Professor professor = new()
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
    }
}
