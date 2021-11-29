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
    public class MySqlExamRegistrationRepository : IExamRegistrationRepository
    {
        public string TableName { get; set; } = "examregistrations";
        public MySqlConnection Connection { get; set; }
        public MySqlTransaction Transaction { get; set; }

        public ExamRegistration Add(ExamRegistration examRegistration)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"INSERT INTO {TableName} (StudentId, ProfessorId, SubjectId, RegistrationDate," +
                    $" ExamDate, Grade, IsLocked) " +
                    $"VALUES ({examRegistration.Student.Id}, {((examRegistration.Professor!=null)?examRegistration.Professor.Id:"NULL")}, " +
                    $"{examRegistration.Subject.Id}, '{examRegistration.RegistrationDate:yyyy-MM-dd}', " +
                    $"{((examRegistration.ExamDate!=null)?"'"+examRegistration.ExamDate.Value.ToString("yyyy-MM-dd") +"'":"NULL")}, {((examRegistration.Grade!=null)?examRegistration.Grade:"NULL")}," +
                    $" {examRegistration.IsLocked} )";

                cmd.ExecuteNonQuery();
                long id = cmd.LastInsertedId;
                examRegistration.Id = id;

                Transaction.Commit();

                return examRegistration;
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

        public ExamRegistration Delete(long id)
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

        public bool Exists(ExamRegistration examRegistration)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT count(*) as count FROM {TableName} WHERE StudentId = {examRegistration.Student.Id} AND SubjectId = {examRegistration.Subject.Id} AND (Grade IS NULL OR Grade > 5 OR IsLocked=0)";

                dataReader = cmd.ExecuteReader();
                dataReader.Read();
                int count = dataReader.GetInt16("count");

                dataReader.Close();

                Transaction.Commit();

                return count > 0;
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

        public ExamRegistration GetById(long id)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT er.ProfessorId as pid, er.Id as erid, er.RegistrationDate, er.ExamDate, er.Grade, er.IsLocked, s.Id as sid, s.Name,s.ESPB,s.Semester, p.FirstName as pfname, p.LastName as plname, p.JMBG as pjmbg, st.Id as stid, st.FirstName as stfname, st.LastName as stlname, st.Indeks as stindex, st.JMBG as stjmbg FROM {TableName} er INNER JOIN subjects s ON (er.SubjectId = s.Id) INNER JOIN students st ON (er.StudentId = st.Id) LEFT JOIN professors p ON (er.ProfessorId = p.Id) WHERE er.Id = {id}";

                dataReader = cmd.ExecuteReader();

                ExamRegistration examRegistration = null;

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    Subject subject = new()
                    {
                        Id = (long)dataReader.GetUInt64("sid"),
                        Name = dataReader.GetString("Name"),
                        ESPB = dataReader.GetInt16("ESPB"),
                        Semester = dataReader.GetInt16("Semester")
                    };

                    Professor professor = null;
                    long? pid;
                    try
                    {
                        pid = (long)dataReader.GetUInt64("pid");
                    }
                    catch (Exception ex)
                    {
                        pid = null;
                    }

                    if (pid != null)
                    {
                        professor = new()
                        {
                            Id = pid.Value,
                            FirstName = dataReader.GetString("pfname"),
                            LastName = dataReader.GetString("plname"),
                            JMBG = dataReader.GetString("pjmbg")
                        };
                    } 

                    Student student = new()
                    {
                        Id = (long)dataReader.GetUInt64("stid"),
                        FirstName = dataReader.GetString("stfname"),
                        LastName = dataReader.GetString("stlname"),
                        JMBG = dataReader.GetString("stjmbg"),
                        Index = dataReader.GetString("stindex")
                    };

                    DateTime? examDate = null;
                    int? grade = null;
                    if (professor != null)
                    {
                        examDate = dataReader.GetDateTime("ExamDate");
                        grade = dataReader.GetInt16("Grade");
                    }

                    examRegistration = new()
                    {
                        Student = student,
                        Professor = professor,
                        Subject = subject,
                        RegistrationDate = dataReader.GetDateTime("RegistrationDate"),
                        ExamDate = examDate,
                        Grade = grade,
                        Id = (long)dataReader.GetUInt64("erid"),
                        IsLocked = dataReader.GetBoolean("IsLocked")
                    };
                }

                dataReader.Close();
                Transaction.Commit();

                return examRegistration;
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

        public IEnumerable<ExamRegistration> GetExamRegistrationsByProfessorId(long professorId)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT er.ProfessorId as pid, er.Id as erid, er.RegistrationDate, er.ExamDate, er.Grade, er.IsLocked, s.Id as sid, s.Name,s.ESPB,s.Semester, p.FirstName as pfname, p.LastName as plname, p.JMBG as pjmbg, st.Id as stid, st.FirstName as stfname, st.LastName as stlname, st.Indeks as stindex, st.JMBG as stjmbg FROM {TableName} er INNER JOIN subjects s ON (er.SubjectId = s.Id) INNER JOIN students st ON (er.StudentId = st.Id) INNER JOIN professors p ON (er.ProfessorId = p.Id) WHERE ProfessorId = {professorId}";

                dataReader = cmd.ExecuteReader();

                List<ExamRegistration> ers = new();

                while (dataReader.Read())
                {
                    Subject subject = new()
                    {
                        Id = (long)dataReader.GetUInt64("sid"),
                        Name = dataReader.GetString("Name"),
                        ESPB = dataReader.GetInt16("ESPB"),
                        Semester = dataReader.GetInt16("Semester")
                    };

                    Professor professor = null;
                    long? id;
                    try
                    {
                        id = (long)dataReader.GetUInt64("pid");
                    }
                    catch (Exception ex)
                    {
                        id = null;
                    }
                    if (id != null)
                    {
                        professor = new()
                        {
                            Id = id.Value,
                            FirstName = dataReader.GetString("pfname"),
                            LastName = dataReader.GetString("plname"),
                            JMBG = dataReader.GetString("pjmbg")
                        };
                    }

                    Student student = new()
                    {
                        Id = (long)dataReader.GetUInt64("stid"),
                        FirstName = dataReader.GetString("stfname"),
                        LastName = dataReader.GetString("stlname"),
                        JMBG = dataReader.GetString("stjmbg"),
                        Index = dataReader.GetString("stindex")
                    };

                    DateTime? examDate = null;
                    int? grade = null;
                    if (professor != null)
                    {
                        examDate = dataReader.GetDateTime("ExamDate");
                        grade = dataReader.GetInt16("Grade");
                    }

                    ExamRegistration examRegistration = new()
                    {
                        Student = student,
                        Professor = professor,
                        Subject = subject,
                        RegistrationDate = dataReader.GetDateTime("RegistrationDate"),
                        ExamDate = examDate,
                        Grade = grade,
                        Id = (long)dataReader.GetUInt64("Id"),
                        IsLocked = dataReader.GetBoolean("IsLocked")
                    };
                    ers.Add(examRegistration);
                }

                dataReader.Close();
                Transaction.Commit();

                return ers;
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

        public IEnumerable<ExamRegistration> GetAll()
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT er.ProfessorId as pid, er.Id as erid, er.RegistrationDate, er.ExamDate, er.Grade, er.IsLocked, s.Id as sid, s.Name,s.ESPB,s.Semester, p.FirstName as pfname, p.LastName as plname, p.JMBG as pjmbg, st.Id as stid, st.FirstName as stfname, st.LastName as stlname, st.Indeks as stindex, st.JMBG as stjmbg FROM {TableName} er INNER JOIN subjects s ON (er.SubjectId = s.Id) INNER JOIN students st ON (er.StudentId = st.Id) LEFT JOIN professors p ON (er.ProfessorId = p.Id)";

                dataReader = cmd.ExecuteReader();

                List<ExamRegistration> ers = new();

                while (dataReader.Read())
                {
                    Subject subject = new()
                    {
                        Id = (long)dataReader.GetUInt64("sid"),
                        Name = dataReader.GetString("Name"),
                        ESPB = dataReader.GetInt16("ESPB"),
                        Semester = dataReader.GetInt16("Semester")
                    };

                    Professor professor = null;
                    long? id;
                    try
                    {
                        id = (long)dataReader.GetUInt64("pid");
                    }
                    catch (Exception ex)
                    {
                        id = null;
                    }

                    if (id != null)
                    {
                        professor = new()
                        {
                            Id = id.Value,
                            FirstName = dataReader.GetString("pfname"),
                            LastName = dataReader.GetString("plname"),
                            JMBG = dataReader.GetString("pjmbg")
                        };
                    }

                    Student student = new()
                    {
                        Id = (long)dataReader.GetUInt64("stid"),
                        FirstName = dataReader.GetString("stfname"),
                        LastName = dataReader.GetString("stlname"),
                        JMBG = dataReader.GetString("stjmbg"),
                        Index = dataReader.GetString("stindex")
                    };

                    DateTime? examDate = null;
                    int? grade = null;
                    if (professor != null)
                    {
                        examDate = dataReader.GetDateTime("ExamDate");
                        grade = dataReader.GetInt16("Grade");
                    }
                    ExamRegistration examRegistration = new()
                    {
                        Student = student,
                        Professor = professor,
                        Subject = subject,
                        Id = (long)dataReader.GetUInt64("erid"),
                        IsLocked = dataReader.GetBoolean("IsLocked"),
                        RegistrationDate = dataReader.GetDateTime("RegistrationDate"),
                        ExamDate = examDate,
                        Grade = grade
                        
                    };
                    ers.Add(examRegistration);
                }

                dataReader.Close();
                Transaction.Commit();

                return ers;
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

        public IEnumerable<ExamRegistration> GetExamRegistrationsByProfessorName(string professorName)
        {
            try
            {
                if (string.IsNullOrEmpty(professorName))
                {
                    return GetAll();
                }

                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT er.ProfessorId as pid, er.Id as erid, er.RegistrationDate, er.ExamDate, er.Grade, er.IsLocked, s.Id as sid, s.Name,s.ESPB,s.Semester, p.FirstName as pfname, p.LastName as plname, p.JMBG as pjmbg, st.Id as stid, st.FirstName as stfname, st.LastName as stlname, st.Indeks as stindex, st.JMBG as stjmbg FROM {TableName} er INNER JOIN subjects s ON (er.SubjectId = s.Id) INNER JOIN students st ON (er.StudentId = st.Id) INNER JOIN professors p ON (er.ProfessorId = p.Id) WHERE ProfessorName LIKE '{professorName}%'";

                dataReader = cmd.ExecuteReader();

                List<ExamRegistration> ers = new();

                while (dataReader.Read())
                {
                    Subject subject = new()
                    {
                        Id = (long)dataReader.GetUInt64("sid"),
                        Name = dataReader.GetString("Name"),
                        ESPB = dataReader.GetInt16("ESPB"),
                        Semester = dataReader.GetInt16("Semester")
                    };

                    Professor professor = null;
                    long? id;
                    try
                    {
                        id = (long)dataReader.GetUInt64("pid");
                    }
                    catch (Exception ex)
                    {
                        id = null;
                    }

                    if (id != null)
                    {
                        professor = new()
                        {
                            Id = id.Value,
                            FirstName = dataReader.GetString("pfname"),
                            LastName = dataReader.GetString("plname"),
                            JMBG = dataReader.GetString("pjmbg")
                        };
                    }

                    Student student = new()
                    {
                        Id = (long)dataReader.GetUInt64("stid"),
                        FirstName = dataReader.GetString("stfname"),
                        LastName = dataReader.GetString("stlname"),
                        JMBG = dataReader.GetString("stjmbg"),
                        Index = dataReader.GetString("stindex")
                    };

                    DateTime? examDate = null;
                    int? grade = null;
                    if (professor != null)
                    {
                        examDate = dataReader.GetDateTime("ExamDate");
                        grade = dataReader.GetInt16("Grade");
                    }

                    ExamRegistration examRegistration = new()
                    {
                        Student = student,
                        Professor = professor,
                        Subject = subject,
                        RegistrationDate = dataReader.GetDateTime("RegistrationDate"),
                        ExamDate = examDate,
                        Grade = grade,
                        Id = (long)dataReader.GetUInt64("Id"),
                        IsLocked = dataReader.GetBoolean("IsLocked")
                    };
                    ers.Add(examRegistration);
                }

                dataReader.Close();
                Transaction.Commit();

                return ers;
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

        public IEnumerable<ExamRegistration> GetExamRegistrationsByStudentId(long studentId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExamRegistration> GetExamRegistrationsByStudentIndex(string studentIndex)
        {
            try
            {
                if (string.IsNullOrEmpty(studentIndex))
                {
                    return GetAll();
                }

                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT er.ProfessorId as pid, er.Id as erid, er.RegistrationDate, er.ExamDate, er.Grade, er.IsLocked, s.Id as sid, s.Name,s.ESPB,s.Semester, p.FirstName as pfname, p.LastName as plname, p.JMBG as pjmbg, st.Id as stid, st.FirstName as stfname, st.LastName as stlname, st.Indeks as stindex, st.JMBG as stjmbg FROM {TableName} er INNER JOIN subjects s ON (er.SubjectId = s.Id) INNER JOIN students st ON (er.StudentId = st.Id) LEFT JOIN professors p ON (er.ProfessorId = p.Id) WHERE st.Indeks LIKE '{studentIndex}%'";

                dataReader = cmd.ExecuteReader();

                List<ExamRegistration> ers = new();

                while (dataReader.Read())
                {
                    Subject subject = new()
                    {
                        Id = (long)dataReader.GetUInt64("sid"),
                        Name = dataReader.GetString("Name"),
                        ESPB = dataReader.GetInt16("ESPB"),
                        Semester = dataReader.GetInt16("Semester")
                    };

                    Professor professor = null;
                    long? id;
                    try
                    {
                        id = (long)dataReader.GetUInt64("pid");
                    }
                    catch (Exception ex)
                    {
                        id = null;
                    }

                    if (id != null)
                    {
                        professor = new()
                        {
                            Id = id.Value,
                            FirstName = dataReader.GetString("pfname"),
                            LastName = dataReader.GetString("plname"),
                            JMBG = dataReader.GetString("pjmbg")
                        };
                    }

                    Student student = new()
                    {
                        Id = (long)dataReader.GetUInt64("stid"),
                        FirstName = dataReader.GetString("stfname"),
                        LastName = dataReader.GetString("stlname"),
                        JMBG = dataReader.GetString("stjmbg"),
                        Index = dataReader.GetString("stindex")
                    };

                    DateTime? examDate = null;
                    int? grade = null;
                    if (professor != null)
                    {
                        examDate = dataReader.GetDateTime("ExamDate");
                        grade = dataReader.GetInt16("Grade");
                    }

                    ExamRegistration examRegistration = new()
                    {
                        Student = student,
                        Professor = professor,
                        Subject = subject,
                        RegistrationDate = dataReader.GetDateTime("RegistrationDate"),
                        ExamDate = examDate,
                        Grade = grade,
                        Id = (long)dataReader.GetUInt64("Id"),
                        IsLocked = dataReader.GetBoolean("IsLocked")
                    };
                    ers.Add(examRegistration);
                }

                dataReader.Close();
                Transaction.Commit();

                return ers;
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

        public IEnumerable<ExamRegistration> GetExamRegistrationsBySubjectId(long subjectId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ExamRegistration> GetExamRegistrationsBySubjectName(string subjectName)
        {
            try
            {
                if (string.IsNullOrEmpty(subjectName))
                {
                    return GetAll();
                }

                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlDataReader dataReader;
                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"SELECT er.ProfessorId as pid, er.Id as erid, er.RegistrationDate, er.ExamDate, er.Grade, er.IsLocked, s.Id as sid, s.Name,s.ESPB,s.Semester, p.FirstName as pfname, p.LastName as plname, p.JMBG as pjmbg, st.Id as stid, st.FirstName as stfname, st.LastName as stlname, st.Indeks as stindex, st.JMBG as stjmbg FROM {TableName} er INNER JOIN subjects s ON (er.SubjectId = s.Id) INNER JOIN students st ON (er.StudentId = st.Id) LEFT JOIN professors p ON (er.ProfessorId = p.Id) WHERE s.Name LIKE '{subjectName}%'";

                dataReader = cmd.ExecuteReader();

                List<ExamRegistration> ers = new();

                while (dataReader.Read())
                {
                    Subject subject = new()
                    {
                        Id = (long)dataReader.GetUInt64("sid"),
                        Name = dataReader.GetString("Name"),
                        ESPB = dataReader.GetInt16("ESPB"),
                        Semester = dataReader.GetInt16("Semester")
                    };

                    Professor professor = null;
                    long? id;
                    try
                    {
                        id = (long)dataReader.GetUInt64("pid");
                    }
                    catch (Exception ex)
                    {
                        id = null;
                    }

                    if (id != null)
                    {
                        professor = new()
                        {
                            Id = id.Value,
                            FirstName = dataReader.GetString("pfname"),
                            LastName = dataReader.GetString("plname"),
                            JMBG = dataReader.GetString("pjmbg")
                        };
                    }

                    Student student = new()
                    {
                        Id = (long)dataReader.GetUInt64("stid"),
                        FirstName = dataReader.GetString("stfname"),
                        LastName = dataReader.GetString("stlname"),
                        JMBG = dataReader.GetString("stjmbg"),
                        Index = dataReader.GetString("stindex")
                    };

                    DateTime? examDate = null;
                    int? grade = null;
                    if (professor != null)
                    {
                        examDate = dataReader.GetDateTime("ExamDate");
                        grade = dataReader.GetInt16("Grade");
                    }

                    ExamRegistration examRegistration = new()
                    {
                        Student = student,
                        Professor = professor,
                        Subject = subject,
                        RegistrationDate = dataReader.GetDateTime("RegistrationDate"),
                        ExamDate = examDate,
                        Grade = grade,
                        Id = (long)dataReader.GetUInt64("Id"),
                        IsLocked = dataReader.GetBoolean("IsLocked")
                    };
                    ers.Add(examRegistration);
                }

                dataReader.Close();
                Transaction.Commit();

                return ers;
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

        public ExamRegistration Update(ExamRegistration er)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"UPDATE {TableName} SET ProfessorId = {er.Professor.Id}, Grade = {er.Grade}, IsLocked = {er.IsLocked}, ExamDate = '{er.ExamDate.Value.ToString("yyyy-MM-dd")}' WHERE Id = {er.Id}";
                cmd.ExecuteNonQuery();
                
                Transaction.Commit();

                return er;
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

        public void Lock(long id)
        {
            try
            {
                Connection = DbBroker.GetConnection();
                Connection.Open();

                Transaction = Connection.BeginTransaction();

                MySqlCommand cmd = Connection.CreateCommand();

                cmd.CommandText = $"UPDATE {TableName} SET IsLocked = 1 WHERE Id = {id}";
                cmd.ExecuteNonQuery();

                Transaction.Commit();
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
