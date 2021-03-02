using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BakalarPrace.Services;
using BakalarPrace.ExceptionModel;
using MySqlConnector;
using BakalarPrace.Models;

namespace BakalarPrace.Data
{
    public class Database
    {
        public string Message { get; set; }

        public bool IsConnected { get; set; }

        private static Alerter _alerter { get; }
        

        public Database()
        {
            //Zkontroluj připojení k DB
            this._verifyConnection();
        }

        public List<Record> GetFourLatestRecords(int authorId)
        {
            if(this.VerifyUserExistenceByID(authorId) == false)
            {
                throw new Exception("GetFourLatestRecords: Tento uživatel neexistuje");
            }

            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT ID, AuthorID, Uploaded, Location, Name FROM record WHERE AuthorId = @id ORDER BY ID DESC LIMIT 4";
                        cmd.Parameters.AddWithValue("@id", authorId);
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            List<Record> records = new List<Record>();
                            int Id;
                            int Aid;
                            DateTime Uploaded;
                            string Loc;
                            string Name;
                            while (reader.Read())
                            {
                                try { Id = reader.GetInt32(0); } catch (Exception) { Id = 0; }
                                try { Aid = reader.GetInt32(1); } catch (Exception) { Aid = 0; }
                                try { Uploaded = reader.GetDateTime(2); } catch (Exception) { Uploaded = DateTime.Now; }
                                try { Loc = reader.GetString(3); } catch (Exception) { Loc = null; }
                                try { Name = reader.GetString(4); } catch (Exception) { Name = null; }

                                records.Add(new Record(Id, Name, Aid, Uploaded, Loc));
                            }
                            return records;
                        }
                        else
                        {
                            return new List<Record>();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Nastala chyba při výpisu records: " + ex.Message);
                    return new List<Record>();
                }
            }
        }

        public User GetUserByID(int ID)
        {
            using(var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using(var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT ID, Firstname, Surname, Email, Level FROM user WHERE ID = @id";
                        cmd.Parameters.AddWithValue("@id", ID);
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int id;
                            string fname;
                            string sname;
                            string email;
                            string level;
                            while (reader.Read())
                            {
                                try { id = reader.GetInt32(0); } catch (Exception) { id = 0; }
                                try { fname = reader.GetString(1); } catch (Exception) { fname = null; }
                                try { sname = reader.GetString(2); } catch (Exception) { sname = null; }
                                try { email = reader.GetString(3); } catch (Exception) { email = null; }
                                try { level = reader.GetString(4); } catch (Exception) { level = null; }
                                return new User(id, fname, sname, email, level);
                            }
                        }                      
                        return new User();
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine("Došlo k chybě při získání uživatele z databáze: "+ex.Message);
                    return new User();
                }
            }
        }

        public List<User> GetAllUsers()
        {
            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT ID, Firstname, Surname, Email, Level FROM user;";
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int id;
                            string fname;
                            string sname;
                            string email;
                            string level;
                            List<User> usrs = new List<User>();
                            while (reader.Read())
                            {
                                try { id = reader.GetInt32(0); } catch (Exception) { id = 0; }
                                try { fname = reader.GetString(1); } catch (Exception) { fname = null; }
                                try { sname = reader.GetString(2); } catch (Exception) { sname = null; }
                                try { email = reader.GetString(3); } catch (Exception) { email = null; }
                                try { level = reader.GetString(4); } catch (Exception) { level = null; }
                                usrs.Add(new User(id, fname, sname, email, level));
                            }
                            return usrs;
                        }
                        return new List<User>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Došlo k chybě při získání uživatele z databáze: " + ex.Message);
                    return new List<User>();
                }
            }
        }

        public User GetUserByEmail(string Email)
        {
            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT ID, Firstname, Surname, Email, Level FROM user WHERE Email = @email";
                        cmd.Parameters.AddWithValue("@email", Email);
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int id;
                            string fname;
                            string sname;
                            string email;
                            string level;
                            while (reader.Read())
                            {
                                try { id = reader.GetInt32(0); } catch (Exception) { id = 0; }
                                try { fname = reader.GetString(1); } catch (Exception) { fname = null; }
                                try { sname = reader.GetString(2); } catch (Exception) { sname = null; }
                                try { email = reader.GetString(3); } catch (Exception) { email = null; }
                                try { level = reader.GetString(4); } catch (Exception) { level = null; }
                                return new User(id, fname, sname, email, level);
                            }
                        }
                        return new User();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Došlo k chybě při získání uživatele z databáze: " + ex.Message);
                    return new User();
                }
            }
        }

        public bool RegisterUser(User user)
        {
            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "INSERT INTO user (Firstname, Surname, Email, Level, Password) VALUES (@fname, @sname, @email, @level, @password)";
                        cmd.Parameters.AddWithValue("@email", user.Email);
                        cmd.Parameters.AddWithValue("@password", user.Password);
                        cmd.Parameters.AddWithValue("@fname", user.Firstname);
                        cmd.Parameters.AddWithValue("@sname", user.Lastname);
                        cmd.Parameters.AddWithValue("@level", user.Level);
                        var reader = cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Nastala chyba při registrace uživatele: " + ex.Message);
                    return false;
                }
            }
        }
        public bool VerifyRecordOwner(int userId)
        {
            using(var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using(var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT 1 FROM Record WHERE AuthorID = @aid";
                        cmd.Parameters.AddWithValue("@aid", userId);
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public bool VerifyUserExistenceByEmail(string email)
        {
            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT 1 FROM user WHERE Email = @email";
                        cmd.Parameters.AddWithValue("@email", email);
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Došlo k chybě při ověření existence uživatele v databázi: " + ex.Message);
                    return false;
                }
            }
        }

        public bool VerifyUserExistenceByID(int id)
        {
            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT 1 FROM user WHERE ID = @id";
                        cmd.Parameters.AddWithValue("@id", id);
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Došlo k chybě při ověření existence uživatele v databázi: " + ex.Message);
                    return false;
                }
            }
        }

        public User LoginUser(string email, string password)
        {
            using(var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using(var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT ID, Firstname, Surname, Email, Level FROM user WHERE Email = @email AND Password = @pswd";
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@pswd", password);
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int id = 0;
                            string fname = null;
                            string sname = null;
                            string Email = null;
                            string level = null;
                            while (reader.Read())
                            {
                                try { id = reader.GetInt32(0); } catch (Exception) { id = 0; }
                                try { fname = reader.GetString(1); } catch (Exception) { fname = null; }
                                try { sname = reader.GetString(2); } catch (Exception) { sname = null; }
                                try { Email = reader.GetString(3); } catch (Exception) { Email = null; }
                                try { level = reader.GetString(4); } catch (Exception) { level = null; }

                            }
                            return new User(id, fname, sname, Email, level);

                        }
                        else
                        {
                            return new User();
                        }
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine("Nastala chyba při loginu uživatele: " + ex.Message);
                    return new User();
                }
            }
        }

        public List<Record> GetRecords(int authorId)
        {
            if(this.VerifyUserExistenceByID(authorId) == false)
            {
                throw new Exception("Funkce: GetRecords: Tento uživatel neexistuje!");
            }
            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using(var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT ID, AuthorID, Uploaded, Location, Name FROM record WHERE AuthorID = @id ORDER BY ID DESC";
                        cmd.Parameters.AddWithValue("@id", authorId);
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            List<Record> records = new List<Record>();
                            int Id;
                            int Aid;
                            DateTime Uploaded;
                            string Loc;
                            string Name;
                            while (reader.Read())
                            {
                                try { Id = reader.GetInt32(0); } catch (Exception) { Id = 0; }
                                try { Aid = reader.GetInt32(1); } catch (Exception) { Aid = 0; }
                                try { Uploaded = reader.GetDateTime(2); } catch (Exception) { Uploaded = DateTime.Now; }
                                try { Loc = reader.GetString(3); } catch (Exception) { Loc = null; }
                                try { Name = reader.GetString(4); } catch (Exception) { Name = null; }

                                records.Add(new Record(Id, Name, Aid, Uploaded, Loc));
                            }
                            return records;
                        }
                        else
                        {
                            return new List<Record>();
                        }
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine("Nastala chyba při výpisu records: "+ex.Message);
                    return new List<Record>();
                }
            }
        }

        public List<Record> GetAllRecords()
        {

            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT ID, AuthorID, Uploaded, Location, Name FROM record ORDER BY ID DESC";
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            List<Record> records = new List<Record>();
                            int Id;
                            int Aid;
                            DateTime Uploaded;
                            string Loc;
                            string Name;
                            while (reader.Read())
                            {
                                try { Id = reader.GetInt32(0); } catch (Exception) { Id = 0; }
                                try { Aid = reader.GetInt32(1); } catch (Exception) { Aid = 0; }
                                try { Uploaded = reader.GetDateTime(2); } catch (Exception) { Uploaded = DateTime.Now; }
                                try { Loc = reader.GetString(3); } catch (Exception) { Loc = null; }
                                try { Name = reader.GetString(4); } catch (Exception) { Name = null; }

                                records.Add(new Record(Id, Name, Aid, Uploaded, Loc));
                            }
                            return records;
                        }
                        else
                        {
                            return new List<Record>();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Nastala chyba při výpisu records: " + ex.Message);
                    return new List<Record>();
                }
            }
        }

        public Record GetDetailedRecord(int recordId)
        {
            if (this.VerifyRecordExistenceById(recordId) == false)
            {
                return new Record();
            }

            using(var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using(var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT ID, AuthorID, Uploaded, Location, Name FROM record WHERE ID = @id";
                        cmd.Parameters.AddWithValue("@id", recordId);
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int Id = 0;
                            int Aid = 0;
                            DateTime Uploaded = DateTime.Now;
                            string Loc = null;
                            string Name = null;
                            while (reader.Read())
                            {
                                try { Id = reader.GetInt32(0); } catch (Exception) { Id = 0; }
                                try { Aid = reader.GetInt32(1); } catch (Exception) { Aid = 0; }
                                try { Uploaded = reader.GetDateTime(2); } catch (Exception) { Uploaded = DateTime.Now; }
                                try { Loc = reader.GetString(3); } catch (Exception) { Loc = null; }
                                try { Name = reader.GetString(4); } catch (Exception) { Name = null; }
                            }
                            List<CsvRow> csv = this.GetCSV(Id);
                            return new Record(Id, Name, Aid, Uploaded, Loc, csv);
                            
                        }
                        else
                        {
                            return new Record();
                        }
                    
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine("Při získání detailního výpisu došlo k chybě: "+ex.Message);
                    return new Record();
                }
            }
        }

        public LogMessage RemoveReport(int recordId)
        {
            if (this.VerifyRecordExistenceById(recordId) == false)
            {
                return new LogMessage("Odstranění záznamu", "404", "Tento záznam nebyl v databázi nalezen", "Error");
            }

            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "DELETE FROM CSV WHERE CSV.RecordID = @recordId";
                        cmd.Parameters.AddWithValue("@recordId", recordId);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "DELETE FROM Record WHERE Record.ID = @recordId";
                        cmd.Parameters.AddWithValue("@recordId", recordId);
                        cmd.ExecuteNonQuery();
                    }
                    return new LogMessage("Odstranění záznamu", "200", "Záznam byl úspěšně odstraněn", "OK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new LogMessage("Odstranění záznamu", "500", "Nastala chyba při odstranění záznamu", "ERROR");
                }
            }
        }


        public LogMessage RemoveUserReport(int recordId, int userId)
        {
            if(this.VerifyRecordExistenceById(recordId) == false)
            {
                return new LogMessage("Odstranění záznamu", "404", "Tento záznam nebyl v databázi nalezen", "Error");
            }

            if (this.VerifyRecordOwner(userId) == false)
            {
                return new LogMessage("Odstranění záznamu", "500", "Tento záznam může odstranit pouze jeho autor", "Error");
            }

            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using(var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "DELETE FROM CSV WHERE CSV.RecordID = @recordId";
                        cmd.Parameters.AddWithValue("@recordId", recordId);
                        cmd.ExecuteNonQuery();
                    }

                    using(var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "DELETE FROM Record WHERE Record.ID = @recordId";
                        cmd.Parameters.AddWithValue("@recordId", recordId);
                        cmd.ExecuteNonQuery();
                    }
                    return new LogMessage("Odstranění záznamu", "200", "Záznam byl úspěšně odstraněn", "OK");
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);   
                    return new LogMessage("Odstranění záznamu", "500", "Nastala chyba při odstranění záznamu", "ERROR");
                }
            }
        }

        public bool CheckForAdminUser()
        {
            using (var db = new AppDb())
            {   
                db.Connection.Open();
                try
                {
                    using (var  cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT 1 FROM user WHERE Level = @lvl";
                        cmd.Parameters.AddWithValue("@lvl", "Admin");
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        public LogMessage RemoveUser(int userId)
        {
            if (this.VerifyUserExistenceByID(userId) == false)
            {
                return new LogMessage("Odstranění uživatele", "404", "Tento uživatel nebyl v databázi nalezen", "Error");
            }

            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "DELETE FROM user WHERE user.ID = @id";
                        cmd.Parameters.AddWithValue("@id", userId);
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "DELETE FROM CSV WHERE RecordID IN ((SELECT ID FROM Record WHERE AuthorID = @id)); DELETE FROM Record WHERE AuthorID = @id;";
                        cmd.Parameters.AddWithValue("@id", userId);
                        cmd.ExecuteNonQuery();
                    }

                    return new LogMessage("Odstranění uživatele", "200", "Uživatel byl úspěšně odstraněn", "OK");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new LogMessage("Odstranění uživatele", "500", "Nastala chyba při pokusu o odstranění uživatele", "ERROR");
                }
            }
        }

        public LogMessage AddReport(int userId, string name, string location, List<CsvRow> csv)
        {
            using(var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using(var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "INSERT INTO record (Name, AuthorID, Location) VALUES (@name, @aid, @location)";
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@aid", userId);
                        cmd.Parameters.AddWithValue("@location", location);
                        cmd.ExecuteNonQuery();

                        Console.WriteLine("Byl přidán Record záznam, pokouším se vložit CSV");
                        //Přidat CSV import
                        int rowIndex = 1;
                        Console.WriteLine(csv.Count());
                        foreach(CsvRow row in csv)
                        {
                            Console.WriteLine("Pokus o provedení vložení # " + rowIndex);
                            this.AddCSV(cmd.LastInsertedId, rowIndex, row);
                            Console.WriteLine(rowIndex);
                            rowIndex++;                         
                        }

                        Console.WriteLine("CSV přidáno");
                        return new LogMessage("Přidání záznamu", "200", "Záznam byl úspěšně přidán", "OK");
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new LogMessage("Přidání záznamu", "500", "Nastala kritická chyba", "ERROR");
                }
            }
        }

        public bool AddCSV(long recordID, int rowKey, CsvRow row)
        {
            int recId = unchecked((int)recordID);
            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "INSERT INTO `csv`(`RecordID`, `RowNumber`, `Date`, `Time`, `Result`, `Mod`, `Best_Match`, `Best_Match_MN`, `Best_Match_2`, `Best_Match_2_MN`, `Best_Match_3`, `Best_Match_3_MN`, `Time_1`, `Time_2`, `Time_all`, `LOD Sigma`, `Key1`, `Value1`, `Key2`, `Value2`, `Key3`, `Value3`, `Key4`, `Value4`, `Key5`, `Value5`, `Key6`, `Value6`, `Key7`, `Value7`, `Key8`, `Value8`, `Key9`, `Value9`, `Key10`, `Value10`, `Key11`, `Value11`, `Key12`, `Value12`, `Key13`, `Value13`, `Key14`, `Value14`, `Key15`, `Value15`, `Key16`, `Value16`, `Mg`, `Mg_Advanced`, `Al`, `Al_Advanced`, `Si`, `Si_Advanced`, `P`, `P_Advanced`, `S`, `S_Advanced`, `Cl`, `Cl_Advanced`, `K`, `K_Advanced`, `Ca`, `Ca_Advanced`, `Ti`, `Ti_Advanced`, `Cr`, `Cr_Advanced`, `Mn`, `Mn_Advanced`, `Fe`, `Fe_Advanced`, `Co`, `Co_Advanced`, `Ni`, `Ni_Advanced`, `Cu`, `Cu_Advanced`, `Zn`, `Zn_Advanced`, `As`, `As_Advanced`, `Se`, `Se_Advanced`, `Rb`, `Rb_Advanced`, `Sr`, `Sr_Advanced`, `Y`, `Y_Advanced`, `Zr`, `Zr_Advanced`, `Mo`, `Mo_Advanced`, `Ag`, `Ag_Advanced`, `Cd`, `Cd_Advanced`, `In`, `In_Advanced`, `Sn`, `Sn_Advanced`, `Sb`, `Sb_Advanced`, `Ba`, `Ba_Advanced`, `W`, `W_Advanced`, `Au`, `Au_Advanced`, `Hg`, `Hg_Advanced`, `Tl`, `Tl_Advanced`, `Pb`, `Pb_Advanced`, `Bi`, `Bi_Advanced`, `Th`, `Th_Advanced`, `U`, `U_Advanced`, `LE`, `LE_Advanced`) VALUES (@par0, @par1, @par2,@par3,@par4,@par5,@par6,@par7,@par8,@par9,@par10,@par11,@par12,@par13,@par14,@par15,@par16,@par17,@par18,@par19,@par20,@par21,@par22,@par23,@par24,@par25,@par26,@par27,@par28,@par29,@par30,@par31,@par32,@par33,@par34,@par35,@par36,@par37,@par38,@par39,@par40,@par41,@par42,@par43,@par44,@par45,@par46,@par47,@par48,@par49,@par50,@par51,@par52,@par53,@par54,@par55,@par56,@par57,@par58,@par59,@par60,@par61,@par62,@par63,@par64,@par65,@par66,@par67,@par68,@par69,@par70,@par71,@par72,@par73,@par74,@par75,@par76,@par77,@par78,@par79,@par80,@par81,@par82,@par83,@par84,@par85,@par86,@par87,@par88,@par89,@par90,@par91,@par92,@par93,@par94,@par95,@par96,@par97,@par98,@par99,@par100,@par101,@par102,@par103,@par104,@par105,@par106,@par107,@par108,@par109,@par110,@par111,@par112,@par113,@par114,@par115,@par116,@par117,@par118,@par119,@par120,@par121,@par122,@par123)";
                        cmd.Parameters.AddWithValue("@par0", recId);
                        cmd.Parameters.AddWithValue("@par1", rowKey);
                        cmd.Parameters.AddWithValue("@par2", row.Datum);
                        cmd.Parameters.AddWithValue("@par3", row.Cas);
                        cmd.Parameters.AddWithValue("@par4", row.Vysledek);
                        cmd.Parameters.AddWithValue("@par5", row.Mod);
                        cmd.Parameters.AddWithValue("@par6", row.Nejlepsi_shoda);
                        cmd.Parameters.AddWithValue("@par7", row.Nejlepsi_shoda_MN);
                        cmd.Parameters.AddWithValue("@par8", row.Nejlepsi_shoda2);
                        cmd.Parameters.AddWithValue("@par9", row.Nejlepsi_shoda2_MN);
                        cmd.Parameters.AddWithValue("@par10", row.Nejlepsi_shoda3);
                        cmd.Parameters.AddWithValue("@par11", row.Nejlepsi_shoda3_MN);
                        cmd.Parameters.AddWithValue("@par12", row.Uplynuly_cas_1);
                        cmd.Parameters.AddWithValue("@par13", row.Uplynuly_cas_2);
                        cmd.Parameters.AddWithValue("@par14", row.Cas_celkem);
                        cmd.Parameters.AddWithValue("@par15", row.LOD);
                        cmd.Parameters.AddWithValue("@par16", row.Pole1);
                        cmd.Parameters.AddWithValue("@par17", row.Pole1_Hodnota);
                        cmd.Parameters.AddWithValue("@par18", row.Pole2);
                        cmd.Parameters.AddWithValue("@par19", row.Pole2_Hodnota);
                        cmd.Parameters.AddWithValue("@par20", row.Pole3);
                        cmd.Parameters.AddWithValue("@par21", row.Pole3_Hodnota);
                        cmd.Parameters.AddWithValue("@par22", row.Pole4);
                        cmd.Parameters.AddWithValue("@par23", row.Pole4_Hodnota);
                        cmd.Parameters.AddWithValue("@par24", row.Pole5);
                        cmd.Parameters.AddWithValue("@par25", row.Pole5_Hodnota);
                        cmd.Parameters.AddWithValue("@par26", row.Pole6);
                        cmd.Parameters.AddWithValue("@par27", row.Pole6_Hodnota);
                        cmd.Parameters.AddWithValue("@par28", row.Pole7);
                        cmd.Parameters.AddWithValue("@par29", row.Pole7_Hodnota);
                        cmd.Parameters.AddWithValue("@par30", row.Pole8);
                        cmd.Parameters.AddWithValue("@par31", row.Pole8_Hodnota);
                        cmd.Parameters.AddWithValue("@par32", row.Pole9);
                        cmd.Parameters.AddWithValue("@par33", row.Pole9_Hodnota);
                        cmd.Parameters.AddWithValue("@par34", row.Pole10);
                        cmd.Parameters.AddWithValue("@par35", row.Pole10_Hodnota);
                        cmd.Parameters.AddWithValue("@par36", row.Pole11);
                        cmd.Parameters.AddWithValue("@par37", row.Pole11_Hodnota);
                        cmd.Parameters.AddWithValue("@par38", row.Pole12);
                        cmd.Parameters.AddWithValue("@par39", row.Pole12_Hodnota);
                        cmd.Parameters.AddWithValue("@par40", row.Pole13);
                        cmd.Parameters.AddWithValue("@par41", row.Pole13_Hodnota);
                        cmd.Parameters.AddWithValue("@par42", row.Pole14);
                        cmd.Parameters.AddWithValue("@par43", row.Pole14_Hodnota);
                        cmd.Parameters.AddWithValue("@par44", row.Pole15);
                        cmd.Parameters.AddWithValue("@par45", row.Pole15_Hodnota);
                        cmd.Parameters.AddWithValue("@par46", row.Pole16);
                        cmd.Parameters.AddWithValue("@par47", row.Pole16_Hodnota);
                        cmd.Parameters.AddWithValue("@par48", row.Mg);
                        cmd.Parameters.AddWithValue("@par49", row.Mg_Advanced);
                        cmd.Parameters.AddWithValue("@par50", row.Al);
                        cmd.Parameters.AddWithValue("@par51", row.Al_Advanced);
                        cmd.Parameters.AddWithValue("@par52", row.Si);
                        cmd.Parameters.AddWithValue("@par53", row.Si_Advanced);
                        cmd.Parameters.AddWithValue("@par54", row.P);
                        cmd.Parameters.AddWithValue("@par55", row.P_Advanced);
                        cmd.Parameters.AddWithValue("@par56", row.S);
                        cmd.Parameters.AddWithValue("@par57", row.S_Advanced);
                        cmd.Parameters.AddWithValue("@par58", row.Cl);
                        cmd.Parameters.AddWithValue("@par59", row.Cl_Advanced);
                        cmd.Parameters.AddWithValue("@par60", row.K);
                        cmd.Parameters.AddWithValue("@par61", row.K_Advanced);
                        cmd.Parameters.AddWithValue("@par62", row.Ca);
                        cmd.Parameters.AddWithValue("@par63", row.Ca_Advanced);
                        cmd.Parameters.AddWithValue("@par64", row.Ti);
                        cmd.Parameters.AddWithValue("@par65", row.Ti_Advanced);
                        cmd.Parameters.AddWithValue("@par66", row.Cr);
                        cmd.Parameters.AddWithValue("@par67", row.Cr_Advanced);
                        cmd.Parameters.AddWithValue("@par68", row.Mn);
                        cmd.Parameters.AddWithValue("@par69", row.Mn_Advanced);
                        cmd.Parameters.AddWithValue("@par70", row.Fe);
                        cmd.Parameters.AddWithValue("@par71", row.Fe_Advanced);
                        cmd.Parameters.AddWithValue("@par72", row.Co);
                        cmd.Parameters.AddWithValue("@par73", row.Co_Advanced);
                        cmd.Parameters.AddWithValue("@par74", row.Ni);
                        cmd.Parameters.AddWithValue("@par75", row.Ni_Advanced);
                        cmd.Parameters.AddWithValue("@par76", row.Cu);
                        cmd.Parameters.AddWithValue("@par77", row.Cu_Advanced);
                        cmd.Parameters.AddWithValue("@par78", row.Zn);
                        cmd.Parameters.AddWithValue("@par79", row.Zn_Advanced);
                        cmd.Parameters.AddWithValue("@par80", row.As);
                        cmd.Parameters.AddWithValue("@par81", row.As_Advanced);
                        cmd.Parameters.AddWithValue("@par82", row.Se);
                        cmd.Parameters.AddWithValue("@par83", row.Se_Advanced);
                        cmd.Parameters.AddWithValue("@par84", row.Rb);
                        cmd.Parameters.AddWithValue("@par85", row.Rb_Advanced);
                        cmd.Parameters.AddWithValue("@par86", row.Sr);
                        cmd.Parameters.AddWithValue("@par87", row.Sr_Advanced);
                        cmd.Parameters.AddWithValue("@par88", row.Y);
                        cmd.Parameters.AddWithValue("@par89", row.Y_Advanced);
                        cmd.Parameters.AddWithValue("@par90", row.Zr);
                        cmd.Parameters.AddWithValue("@par91", row.Zr_Advanced);
                        cmd.Parameters.AddWithValue("@par92", row.Mo);
                        cmd.Parameters.AddWithValue("@par93", row.Mo_Advanced);
                        cmd.Parameters.AddWithValue("@par94", row.Ag);
                        cmd.Parameters.AddWithValue("@par95", row.Ag_Advanced);
                        cmd.Parameters.AddWithValue("@par96", row.Cd);
                        cmd.Parameters.AddWithValue("@par97", row.Cd_Advanced);
                        cmd.Parameters.AddWithValue("@par98", row.In);
                        cmd.Parameters.AddWithValue("@par99", row.In_Advanced);
                        cmd.Parameters.AddWithValue("@par100", row.Sn);
                        cmd.Parameters.AddWithValue("@par101", row.Sn_Advanced);
                        cmd.Parameters.AddWithValue("@par102", row.Sb);
                        cmd.Parameters.AddWithValue("@par103", row.Sb_Advanced);
                        cmd.Parameters.AddWithValue("@par104", row.Ba);
                        cmd.Parameters.AddWithValue("@par105", row.Ba_Advanced);
                        cmd.Parameters.AddWithValue("@par106", row.W);
                        cmd.Parameters.AddWithValue("@par107", row.W_Advanced);
                        cmd.Parameters.AddWithValue("@par108", row.Au);
                        cmd.Parameters.AddWithValue("@par109", row.Au_Advanced);
                        cmd.Parameters.AddWithValue("@par110", row.Hg);
                        cmd.Parameters.AddWithValue("@par111", row.Hg_Advanced);
                        cmd.Parameters.AddWithValue("@par112", row.Tl);
                        cmd.Parameters.AddWithValue("@par113", row.Tl_Advanced);
                        cmd.Parameters.AddWithValue("@par114", row.Pb);
                        cmd.Parameters.AddWithValue("@par115", row.Pb_Advanced);
                        cmd.Parameters.AddWithValue("@par116", row.Bi);
                        cmd.Parameters.AddWithValue("@par117", row.Bi_Advanced);
                        cmd.Parameters.AddWithValue("@par118", row.Th);
                        cmd.Parameters.AddWithValue("@par119", row.Th_Advanced);
                        cmd.Parameters.AddWithValue("@par120", row.U);
                        cmd.Parameters.AddWithValue("@par121", row.U_Advanced);
                        cmd.Parameters.AddWithValue("@par122", row.LE);
                        cmd.Parameters.AddWithValue("@par123", row.LE_Advanced);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Proběhlo uložení do DB");
                        return true;
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine("Nastala chyba :/ : "+ex.Message);
                    return false;
                }
            }
        }

        public List<CsvRow> GetCSV(int recordID)
        {
            List<CsvRow> csv = new List<CsvRow>();
            using (var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT ID, `RowNumber`, `Date`, `Time`, `Result`, `Mod`, `Best_Match`, `Best_Match_MN`, `Best_Match_2`, `Best_Match_2_MN`, `Best_Match_3`, `Best_Match_3_MN`, `Time_1`, `Time_2`, `Time_all`, `LOD Sigma`, `Key1`, `Value1`, `Key2`, `Value2`, `Key3`, `Value3`, `Key4`, `Value4`, `Key5`, `Value5`, `Key6`, `Value6`, `Key7`, `Value7`, `Key8`, `Value8`, `Key9`, `Value9`, `Key10`, `Value10`, `Key11`, `Value11`, `Key12`, `Value12`, `Key13`, `Value13`, `Key14`, `Value14`, `Key15`, `Value15`, `Key16`, `Value16`, `Mg`, `Mg_Advanced`, `Al`, `Al_Advanced`, `Si`, `Si_Advanced`, `P`, `P_Advanced`, `S`, `S_Advanced`, `Cl`, `Cl_Advanced`, `K`, `K_Advanced`, `Ca`, `Ca_Advanced`, `Ti`, `Ti_Advanced`, `Cr`, `Cr_Advanced`, `Mn`, `Mn_Advanced`, `Fe`, `Fe_Advanced`, `Co`, `Co_Advanced`, `Ni`, `Ni_Advanced`, `Cu`, `Cu_Advanced`, `Zn`, `Zn_Advanced`, `As`, `As_Advanced`, `Se`, `Se_Advanced`, `Rb`, `Rb_Advanced`, `Sr`, `Sr_Advanced`, `Y`, `Y_Advanced`, `Zr`, `Zr_Advanced`, `Mo`, `Mo_Advanced`, `Ag`, `Ag_Advanced`, `Cd`, `Cd_Advanced`, `In`, `In_Advanced`, `Sn`, `Sn_Advanced`, `Sb`, `Sb_Advanced`, `Ba`, `Ba_Advanced`, `W`, `W_Advanced`, `Au`, `Au_Advanced`, `Hg`, `Hg_Advanced`, `Tl`, `Tl_Advanced`, `Pb`, `Pb_Advanced`, `Bi`, `Bi_Advanced`, `Th`, `Th_Advanced`, `U`, `U_Advanced`, `LE`, `LE_Advanced` FROM csv WHERE RecordID = @rId";
                        cmd.Parameters.AddWithValue("@rId", recordID);                    
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            int p0;
                            int p1;
                            string p2;
                            string p3;
                            string p4;
                            string p5;
                            string p6;
                            string p7;
                            string p8;
                            string p9;
                            string p10;
                            string p11;
                            string p12;
                            string p13;
                            string p14;
                            string p15;
                            string p16;
                            string p17;
                            string p18;
                            string p19;
                            string p20;
                            string p21;
                            string p22;
                            string p23;
                            string p24;
                            string p25;
                            string p26;
                            string p27;
                            string p28;
                            string p29;
                            string p30;
                            string p31;
                            string p32;
                            string p33;
                            string p34;
                            string p35;
                            string p36;
                            string p37;
                            string p38;
                            string p39;
                            string p40;
                            string p41;
                            string p42;
                            string p43;
                            string p44;
                            string p45;
                            string p46;
                            string p47;
                            string p48;
                            string p49;
                            string p50;
                            string p51;
                            string p52;
                            string p53;
                            string p54;
                            string p55;
                            string p56;
                            string p57;
                            string p58;
                            string p59;
                            string p60;
                            string p61;
                            string p62;
                            string p63;
                            string p64;
                            string p65;
                            string p66;
                            string p67;
                            string p68;
                            string p69;
                            string p70;
                            string p71;
                            string p72;
                            string p73;
                            string p74;
                            string p75;
                            string p76;
                            string p77;
                            string p78;
                            string p79;
                            string p80;
                            string p81;
                            string p82;
                            string p83;
                            string p84;
                            string p85;
                            string p86;
                            string p87;
                            string p88;
                            string p89;
                            string p90;
                            string p91;
                            string p92;
                            string p93;
                            string p94;
                            string p95;
                            string p96;
                            string p97;
                            string p98;
                            string p99;
                            string p100;
                            string p101;
                            string p102;
                            string p103;
                            string p104;
                            string p105;
                            string p106;
                            string p107;
                            string p108;
                            string p109;
                            string p110;
                            string p111;
                            string p112;
                            string p113;
                            string p114;
                            string p115;
                            string p116;
                            string p117;
                            string p118;
                            string p119;
                            string p120;
                            string p121;
                            string p122;
                            string p123;
                            string p124;
                            string p125;

                            while (reader.Read())
                            {
                                try { p0 = reader.GetInt32(0); } catch (Exception) { p0 = 0; }
                                try { p1 = reader.GetInt32(1); } catch (Exception) { p1 = 0; }
                                try { p2 = reader.GetString(2); } catch (Exception) { p2 = null; }
                                try { p3 = reader.GetString(3); } catch (Exception) { p3 = null; }
                                try { p4 = reader.GetString(4); } catch (Exception) { p4 = null; }
                                try { p5 = reader.GetString(5); } catch (Exception) { p5 = null; }
                                try { p6 = reader.GetString(6); } catch (Exception) { p6 = null; }
                                try { p7 = reader.GetString(7); } catch (Exception) { p7 = null; }
                                try { p8 = reader.GetString(8); } catch (Exception) { p8 = null; }
                                try { p9 = reader.GetString(9); } catch (Exception) { p9 = null; }
                                try { p10 = reader.GetString(10); } catch (Exception) { p10 = null; }
                                try { p11 = reader.GetString(11); } catch (Exception) { p11 = null; }
                                try { p12 = reader.GetString(12); } catch (Exception) { p12 = null; }
                                try { p13 = reader.GetString(13); } catch (Exception) { p13 = null; }
                                try { p14 = reader.GetString(14); } catch (Exception) { p14 = null; }
                                try { p15 = reader.GetString(15); } catch (Exception) { p15 = null; }
                                try { p16 = reader.GetString(16); } catch (Exception) { p16 = null; }
                                try { p17 = reader.GetString(17); } catch (Exception) { p17 = null; }
                                try { p18 = reader.GetString(18); } catch (Exception) { p18 = null; }
                                try { p19 = reader.GetString(19); } catch (Exception) { p19 = null; }
                                try { p20 = reader.GetString(20); } catch (Exception) { p20 = null; }
                                try { p21 = reader.GetString(21); } catch (Exception) { p21 = null; }
                                try { p22 = reader.GetString(22); } catch (Exception) { p22 = null; }
                                try { p23 = reader.GetString(23); } catch (Exception) { p23 = null; }
                                try { p24 = reader.GetString(24); } catch (Exception) { p24 = null; }
                                try { p25 = reader.GetString(25); } catch (Exception) { p25 = null; }
                                try { p26 = reader.GetString(26); } catch (Exception) { p26 = null; }
                                try { p27 = reader.GetString(27); } catch (Exception) { p27 = null; }
                                try { p28 = reader.GetString(28); } catch (Exception) { p28 = null; }
                                try { p29 = reader.GetString(29); } catch (Exception) { p29 = null; }
                                try { p30 = reader.GetString(30); } catch (Exception) { p30 = null; }
                                try { p31 = reader.GetString(31); } catch (Exception) { p31 = null; }
                                try { p32 = reader.GetString(32); } catch (Exception) { p32 = null; }
                                try { p33 = reader.GetString(33); } catch (Exception) { p33 = null; }
                                try { p34 = reader.GetString(34); } catch (Exception) { p34 = null; }
                                try { p35 = reader.GetString(35); } catch (Exception) { p35 = null; }
                                try { p36 = reader.GetString(36); } catch (Exception) { p36 = null; }
                                try { p37 = reader.GetString(37); } catch (Exception) { p37 = null; }
                                try { p38 = reader.GetString(38); } catch (Exception) { p38 = null; }
                                try { p39 = reader.GetString(39); } catch (Exception) { p39 = null; }
                                try { p40 = reader.GetString(40); } catch (Exception) { p40 = null; }
                                try { p41 = reader.GetString(41); } catch (Exception) { p41 = null; }
                                try { p42 = reader.GetString(42); } catch (Exception) { p42 = null; }
                                try { p43 = reader.GetString(43); } catch (Exception) { p43 = null; }
                                try { p44 = reader.GetString(44); } catch (Exception) { p44 = null; }
                                try { p45 = reader.GetString(45); } catch (Exception) { p45 = null; }
                                try { p46 = reader.GetString(46); } catch (Exception) { p46 = null; }
                                try { p47 = reader.GetString(47); } catch (Exception) { p47 = null; }
                                try { p48 = reader.GetString(48); } catch (Exception) { p48 = null; }
                                try { p49 = reader.GetString(49); } catch (Exception) { p49 = null; }
                                try { p50 = reader.GetString(50); } catch (Exception) { p50 = null; }
                                try { p51 = reader.GetString(51); } catch (Exception) { p51 = null; }
                                try { p52 = reader.GetString(52); } catch (Exception) { p52 = null; }
                                try { p53 = reader.GetString(53); } catch (Exception) { p53 = null; }
                                try { p54 = reader.GetString(54); } catch (Exception) { p54 = null; }
                                try { p55 = reader.GetString(55); } catch (Exception) { p55 = null; }
                                try { p56 = reader.GetString(56); } catch (Exception) { p56 = null; }
                                try { p57 = reader.GetString(57); } catch (Exception) { p57 = null; }
                                try { p58 = reader.GetString(58); } catch (Exception) { p58 = null; }
                                try { p59 = reader.GetString(59); } catch (Exception) { p59 = null; }
                                try { p60 = reader.GetString(60); } catch (Exception) { p60 = null; }
                                try { p61 = reader.GetString(61); } catch (Exception) { p61 = null; }
                                try { p62 = reader.GetString(62); } catch (Exception) { p62 = null; }
                                try { p63 = reader.GetString(63); } catch (Exception) { p63 = null; }
                                try { p64 = reader.GetString(64); } catch (Exception) { p64 = null; }
                                try { p65 = reader.GetString(65); } catch (Exception) { p65 = null; }
                                try { p66 = reader.GetString(66); } catch (Exception) { p66 = null; }
                                try { p67 = reader.GetString(67); } catch (Exception) { p67 = null; }
                                try { p68 = reader.GetString(68); } catch (Exception) { p68 = null; }
                                try { p69 = reader.GetString(69); } catch (Exception) { p69 = null; }
                                try { p70 = reader.GetString(70); } catch (Exception) { p70 = null; }
                                try { p71 = reader.GetString(71); } catch (Exception) { p71 = null; }
                                try { p72 = reader.GetString(72); } catch (Exception) { p72 = null; }
                                try { p73 = reader.GetString(73); } catch (Exception) { p73 = null; }
                                try { p74 = reader.GetString(74); } catch (Exception) { p74 = null; }
                                try { p75 = reader.GetString(75); } catch (Exception) { p75 = null; }
                                try { p76 = reader.GetString(76); } catch (Exception) { p76 = null; }
                                try { p77 = reader.GetString(77); } catch (Exception) { p77 = null; }
                                try { p78 = reader.GetString(78); } catch (Exception) { p78 = null; }
                                try { p79 = reader.GetString(79); } catch (Exception) { p79 = null; }
                                try { p80 = reader.GetString(80); } catch (Exception) { p80 = null; }
                                try { p81 = reader.GetString(81); } catch (Exception) { p81 = null; }
                                try { p82 = reader.GetString(82); } catch (Exception) { p82 = null; }
                                try { p83 = reader.GetString(83); } catch (Exception) { p83 = null; }
                                try { p84 = reader.GetString(84); } catch (Exception) { p84 = null; }
                                try { p85 = reader.GetString(85); } catch (Exception) { p85 = null; }
                                try { p86 = reader.GetString(86); } catch (Exception) { p86 = null; }
                                try { p87 = reader.GetString(87); } catch (Exception) { p87 = null; }
                                try { p88 = reader.GetString(88); } catch (Exception) { p88 = null; }
                                try { p89 = reader.GetString(89); } catch (Exception) { p89 = null; }
                                try { p90 = reader.GetString(90); } catch (Exception) { p90 = null; }
                                try { p91 = reader.GetString(91); } catch (Exception) { p91 = null; }
                                try { p92 = reader.GetString(92); } catch (Exception) { p92 = null; }
                                try { p93 = reader.GetString(93); } catch (Exception) { p93 = null; }
                                try { p94 = reader.GetString(94); } catch (Exception) { p94 = null; }
                                try { p95 = reader.GetString(95); } catch (Exception) { p95 = null; }
                                try { p96 = reader.GetString(96); } catch (Exception) { p96 = null; }
                                try { p97 = reader.GetString(97); } catch (Exception) { p97 = null; }
                                try { p98 = reader.GetString(98); } catch (Exception) { p98 = null; }
                                try { p99 = reader.GetString(99); } catch (Exception) { p99 = null; }
                                try { p100 = reader.GetString(100); } catch (Exception) { p100 = null; }
                                try { p101 = reader.GetString(101); } catch (Exception) { p101 = null; }
                                try { p102 = reader.GetString(102); } catch (Exception) { p102 = null; }
                                try { p103 = reader.GetString(103); } catch (Exception) { p103 = null; }
                                try { p104 = reader.GetString(104); } catch (Exception) { p104 = null; }
                                try { p105 = reader.GetString(105); } catch (Exception) { p105 = null; }
                                try { p106 = reader.GetString(106); } catch (Exception) { p106 = null; }
                                try { p107 = reader.GetString(107); } catch (Exception) { p107 = null; }
                                try { p108 = reader.GetString(108); } catch (Exception) { p108 = null; }
                                try { p109 = reader.GetString(109); } catch (Exception) { p109 = null; }
                                try { p110 = reader.GetString(110); } catch (Exception) { p110 = null; }
                                try { p111 = reader.GetString(111); } catch (Exception) { p111 = null; }
                                try { p112 = reader.GetString(112); } catch (Exception) { p112 = null; }
                                try { p113 = reader.GetString(113); } catch (Exception) { p113 = null; }
                                try { p114 = reader.GetString(114); } catch (Exception) { p114 = null; }
                                try { p115 = reader.GetString(115); } catch (Exception) { p115 = null; }
                                try { p116 = reader.GetString(116); } catch (Exception) { p116 = null; }
                                try { p117 = reader.GetString(117); } catch (Exception) { p117 = null; }
                                try { p118 = reader.GetString(118); } catch (Exception) { p118 = null; }
                                try { p119 = reader.GetString(119); } catch (Exception) { p119 = null; }
                                try { p120 = reader.GetString(120); } catch (Exception) { p120 = null; }
                                try { p121 = reader.GetString(121); } catch (Exception) { p121 = null; }
                                try { p122 = reader.GetString(122); } catch (Exception) { p122 = null; }
                                try { p123 = reader.GetString(123); } catch (Exception) { p123 = null; }
                                try { p124 = reader.GetString(124); } catch (Exception) { p124 = null; }
                                try { p125 = reader.GetString(125); } catch (Exception) { p125 = null; }
                                csv.Add(new CsvRow(p0,
                                                    p1,
                                                    p2,
                                                    p3,
                                                    p4,
                                                    p5,
                                                    p6,
                                                    p7,
                                                    p8,
                                                    p9,
                                                    p10,
                                                    p11,
                                                    p12,
                                                    p13,
                                                    p14,
                                                    p15,
                                                    p16,
                                                    p17,
                                                    p18,
                                                    p19,
                                                    p20,
                                                    p21,
                                                    p22,
                                                    p23,
                                                    p24,
                                                    p25,
                                                    p26,
                                                    p27,
                                                    p28,
                                                    p29,
                                                    p30,
                                                    p31,
                                                    p32,
                                                    p33,
                                                    p34,
                                                    p35,
                                                    p36,
                                                    p37,
                                                    p38,
                                                    p39,
                                                    p40,
                                                    p41,
                                                    p42,
                                                    p43,
                                                    p44,
                                                    p45,
                                                    p46,
                                                    p47,
                                                    p48,
                                                    p49,
                                                    p50,
                                                    p51,
                                                    p52,
                                                    p53,
                                                    p54,
                                                    p55,
                                                    p56,
                                                    p57,
                                                    p58,
                                                    p59,
                                                    p60,
                                                    p61,
                                                    p62,
                                                    p63,
                                                    p64,
                                                    p65,
                                                    p66,
                                                    p67,
                                                    p68,
                                                    p69,
                                                    p70,
                                                    p71,
                                                    p72,
                                                    p73,
                                                    p74,
                                                    p75,
                                                    p76,
                                                    p77,
                                                    p78,
                                                    p79,
                                                    p80,
                                                    p81,
                                                    p82,
                                                    p83,
                                                    p84,
                                                    p85,
                                                    p86,
                                                    p87,
                                                    p88,
                                                    p89,
                                                    p90,
                                                    p91,
                                                    p92,
                                                    p93,
                                                    p94,
                                                    p95,
                                                    p96,
                                                    p97,
                                                    p98,
                                                    p99,
                                                    p100,
                                                    p101,
                                                    p102,
                                                    p103,
                                                    p104,
                                                    p105,
                                                    p106,
                                                    p107,
                                                    p108,
                                                    p109,
                                                    p110,
                                                    p111,
                                                    p112,
                                                    p113,
                                                    p114,
                                                    p115,
                                                    p116,
                                                    p117,
                                                    p118,
                                                    p119,
                                                    p120,
                                                    p121,
                                                    p122,
                                                    p123));
                            }
                        }
                        Console.WriteLine("Proběhlo načtení CSV z DB");
                        return csv;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Nastala chyba :/ : " + ex.Message);
                    return csv;
                }
            }
        }

        public bool VerifyRecordExistenceById(int id)
        {
            using(var db = new AppDb())
            {
                try
                {
                    db.Connection.Open();
                    using(var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "SELECT 1 FROM record WHERE ID = @id";
                        cmd.Parameters.AddWithValue("@id", id);
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }

        private void _verifyConnection()
        {
            using(var db = new AppDb())
            {
                try
                {
                    db.Connection.Open();
                    this.Message = "Připojeno";
                    this.IsConnected = true;
                }catch(Exception ex)
                {
                    this.Message = ex.Message;
                    this.IsConnected = false;
                }
            }
        }

        private void _verifyUserTable()
        {
            using(var db = new AppDb())
            {
                db.Connection.Open();
                try
                {
                    using(var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "DESCRIBE user";
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            this.IsConnected = true;
                            this.Message = "OK";
                        }
                        else
                        {
                            this.IsConnected = false;
                            this.Message = "Nebyla nalezena tabulka USER";
                        }
                    }
                }catch(Exception ex)
                {
                    this.Message = ex.Message;
                    this.IsConnected = false;
                }
            }
        }


    }
}
