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

        public async Task<LogMessage> AddReport(int userId, string location, List<CsvRow> csv)
        {
            using(var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                try
                {
                    using(var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "INSERT INTO Record (AuthorID, Location) VALUES (@aid, @location)";
                        cmd.Parameters.AddWithValue("@aid", userId);
                        cmd.Parameters.AddWithValue("@location", location);
                        await cmd.ExecuteNonQueryAsync();

                        //Přidat CSV import
                        int rowIndex = 1;
                        foreach(CsvRow row in csv)
                        {
                            await this.AddCSV(cmd.LastInsertedId, rowIndex, row);
                            rowIndex++;
                        }
                        return new LogMessage("Přidání záznamu", "200", "Záznam byl úspěšně přidán", "OK");
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return new LogMessage("Přidání záznamu", "500", "Nastala kritická chyba", "ERROR");
                }
            }
        }

        public async Task<bool> AddCSV(long recordID, int rowKey, CsvRow row)
        {
            int recId = unchecked((int)recordID);
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                try
                {
                    using (var cmd = new MySqlCommand())
                    {
                        cmd.Connection = db.Connection;
                        cmd.CommandText = "INSERT INTO `csv`(`RecordID`, `RowNumber`, `Result`, `Mod`, `Best_Match`, `Best_Match_MN`, `Best_Match_2`, `Best_Match_2_MN`, `Best_Match_3`, `Best_Match_3_MN`, `Time_1`, `Time_2`, `Time_all`, `LOD Sigma`, `Key1`, `Value1`, `Key2`, `Value2`, `Key3`, `Value3`, `Key4`, `Value4`, `Key5`, `Value5`, `Key6`, `Value6`, `Key7`, `Value7`, `Key8`, `Value8`, `Key9`, `Value9`, `Key10`, `Value10`, `Key11`, `Value11`, `Key12`, `Value12`, `Key13`, `Value13`, `Key14`, `Value14`, `Key15`, `Value15`, `Key16`, `Value16`, `Mg`, `Mg_Advanced`, `Al`, `Al_Advanced`, `Si`, `Si_Advanced`, `P`, `P_Advanced`, `S`, `S_Advanced`, `Cl`, `Cl_Advanced`, `K`, `K_Advanced`, `Ca`, `Ca_Advanced`, `Ti`, `Ti_Advanced`, `Cr`, `Cr_Advanced`, `Mn`, `Mn_Advanced`, `Fe`, `Fe_Advanced`, `Co`, `Co_Advanced`, `Ni`, `Ni_Advanced`, `Cu`, `Cu_Advanced`, `Zn`, `Zn_Advanced`, `As`, `As_Advanced`, `Se`, `Se_Advanced`, `Rb`, `Rb_Advanced`, `Sr`, `Sr_Advanced`, `Y`, `Y_Advanced`, `Zr`, `Zr_Advanced`, `Mo`, `Mo_Advanced`, `Ag`, `Ag_Advanced`, `Cd`, `Cd_Advanced`, `In`, `In_Advanced`, `Sn`, `Sn_Advanced`, `Sb`, `Sb_Advanced`, `Ba`, `Ba_Advanced`, `W`, `W_Advanced`, `Au`, `Au_Advanced`, `Hg`, `Hg_Advanced`, `Tl`, `Tl_Advanced`, `Pb`, `Pb_Advanced`, `Bi`, `Bi_Advanced`, `Th`, `Th_Advanced`, `U`, `U_Advanced`, `LE`, `LE_Advanced`) VALUES (@par2,@par3,@par4,@par5,@par6,@par7,@par8,@par9,@par10,@par11,@par12,@par13,@par14,@par15,@par16,@par17,@par18,@par19,@par20,@par21,@par22,@par23,@par24,@par25,@par26,@par27,@par28,@par29,@par30,@par31,@par32,@par33,@par34,@par35,@par36,@par37,@par38,@par39,@par40,@par41,@par42,@par43,@par44,@par45,@par46,@par47,@par48,@par49,@par50,@par51,@par52,@par53,@par54,@par55,@par56,@par57,@par58,@par59,@par60,@par61,@par62,@par63,@par64,@par65,@par66,@par67,@par68,@par69,@par70,@par71,@par72,@par73,@par74,@par75,@par76,@par77,@par78,@par79,@par80,@par81,@par82,@par83,@par84,@par85,@par86,@par87,@par88,@par89,@par90,@par91,@par92,@par93,@par94,@par95,@par96,@par97,@par98,@par99,@par100,@par101,@par102,@par103,@par104,@par105,@par106,@par107,@par108,@par109,@par110,@par111,@par112,@par113,@par114,@par115,@par116,@par117,@par118,@par119,@par120,@par121,@par122,@par123)";
                        cmd.Parameters.AddWithValue("@par2", recId);
                        cmd.Parameters.AddWithValue("@par3", rowKey);
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
                        await cmd.ExecuteNonQueryAsync();
                        return true;
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine("Nastala chyba :/ : "+ex.Message);
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
                        cmd.CommandText = "DESCRIBE User";
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
