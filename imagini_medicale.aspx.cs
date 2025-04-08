using System;
using System.Collections.Generic;
using System.Data;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;


namespace proiect_imagini_medicale_vasilemiruna
{
    public partial class imagini_medicale : System.Web.UI.Page
    {
        private OracleConnection con;
        protected void Page_Load(object sender, EventArgs e)
        {
            string cons = "User ID=STUD_VASILEM; Password=student; Data Source=(DESCRIPTION=" +
                          "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=37.120.249.41)(PORT=1521)))" +
                          "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcls)));";
            con = new OracleConnection(cons);
        }

        protected void IncarcaImagineaInSistem_Click(object sender, EventArgs e)
        {
            Ecou.Text = "";
            if (incarcareimagine.HasFile)
            {
                try
                {
                    string extension = Path.GetExtension(incarcareimagine.FileName).ToLower();
                    if (extension != ".jpg" && extension != ".png" && extension != ".gif" && extension != ".dicom" && extension != ".dcm")
                    {
                        Ecou.Text = "Tipul de fisier nu este acceptat. Te rugam sa incarci o imagine medicala valida (.jpg, .png, .gif, .dicom, .dcm).";
                        return;
                    }

                    string filePath = Path.Combine(@"C:\Users\Miruu\Desktop\proiect baze multi\imagini medicale\" + incarcareimagine.FileName);
                    incarcareimagine.SaveAs(filePath);
                    Ecou.Text = "Fisierul incarcat este " + incarcareimagine.FileName;

                    byte[] imageBytes = File.ReadAllBytes(filePath);
                    Ecou.Text += " Imaginea medicala incarcata are dimensiunea de : " + imageBytes.Length + " bytes.";
                    DateTime dataIncarcare = DateTime.Now;
                    con.Open();
                    OracleCommand cmd = new OracleCommand("ins_imagine_medicala", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add("vcod", OracleDbType.Int32).Value = Convert.ToInt32(tb_cod.Text);
                    cmd.Parameters.Add("vdescriere", OracleDbType.Varchar2).Value = tb_descriere.Text;
                    cmd.Parameters.Add("vfis", OracleDbType.Blob).Value = imageBytes;
                    cmd.Parameters.Add("vdata_incarcare", OracleDbType.Date).Value = dataIncarcare;
                    cmd.Parameters.Add("vrezultate_analiza", OracleDbType.Varchar2).Value = tb_rezultateanaliza.Text;

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    Ecou.Text += "Imaginea a fost incarcata cu succes.";
                }


                catch (OracleException ex)
                {
                    Ecou.Text += " A aparut o eroare in timpul incarcarii imaginii medicale.  " + ex.Message;
                }
                catch (Exception ex)
                {
                    Ecou.Text += " Eroare generala.Te rugam să selectezi un fisier pentru incarcare.  " + ex.Message;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else
            {
                Ecou.Text = "Fișier imagine lipsă!";
            }
        }

        protected void afisareimagine_Click(object sender, EventArgs e)
        {
            imagineaafisata.ImageUrl = "";
            Ecou.Text = "";
            afisaredescriere.Text = "";
            afisaredataincarcare.Text = "";
            afisarerezultate.Text = "";


            try
            {
                con.Open();
                OracleCommand cmd = new OracleCommand("selectare_afisimagine_medicala", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

             
                cmd.Parameters.Add("vcod", OracleDbType.Int32).Value = Convert.ToInt32(codafisareimagine.Text);

              
                cmd.Parameters.Add("flux", OracleDbType.Blob).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("vdescriere", OracleDbType.Varchar2, 255).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("vdata_incarcare", OracleDbType.Date).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("vrezultate_analiza", OracleDbType.Varchar2, 4000).Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();

    
                if (cmd.Parameters["flux"].Value != DBNull.Value)
                {

                    OracleBlob blob = (OracleBlob)cmd.Parameters["flux"].Value;
                    byte[] imageBytes = new byte[blob.Length];
                    blob.Read(imageBytes, 0, imageBytes.Length);


                    string base64Image = Convert.ToBase64String(imageBytes);
                    imagineaafisata.ImageUrl = "data:image/gif;base64," + base64Image;

                 
                    afisaredescriere.Text = cmd.Parameters["vdescriere"].Value.ToString();
                    DateTime dataIncarcare = ((OracleDate)cmd.Parameters["vdata_incarcare"].Value).Value;
                    afisaredataincarcare.Text = dataIncarcare.ToString("dd-MM-yyyy");

                    afisarerezultate.Text = cmd.Parameters["vrezultate_analiza"].Value.ToString();

                    Ecou.Text = "Imaginea si detaliile au fost afisate cu succes.";
                }
                else
                {
                    Ecou.Text = "Nu s-a gasit nicio imagine pentru codul specificat.";
                }
            }
            catch (OracleException ex)
            {
                Ecou.Text = "Eroare Oracle: " + ex.Message;
            }
            catch (Exception ex)
            {
                Ecou.Text = "Eroare generala: " + ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }


        }

        protected void incarcanucleara_Click(object sender, EventArgs e)
        {
            Ecouu.Text = "";

            if (imaginenucleara.HasFile)
            {
                try
                {
                    string extension = Path.GetExtension(imaginenucleara.FileName).ToLower();
                    if (extension != ".jpg" && extension != ".png" && extension != ".gif" && extension != ".dicom" && extension != ".dcm" && extension != ".jpeg")
                    {
                        Ecouu.Text = "Tipul de fișier nu este acceptat. Te rugăm să încarci o imagine medicală validă (.jpg, .png, .gif, .dicom, .dcm).";
                        return;
                    }

                    string filePath = Path.Combine(@"C:\Users\Miruu\Desktop\proiect baze multi\imagini nucleare\", imaginenucleara.FileName);
                    imaginenucleara.SaveAs(filePath);
                    Ecouu.Text = "Fișierul încărcat este " + imaginenucleara.FileName;

                    byte[] imageBytes = File.ReadAllBytes(filePath);
                    Ecouu.Text += " Imaginea medicală încărcată are dimensiunea de : " + imageBytes.Length + " bytes.";

                    con.Open();


                    int idImagine = Convert.ToInt32(idnuclear.Text);

                    OracleCommand cmd = new OracleCommand("ins_imagine_scintigrafie", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Determinarea substanței selectate
                    string substantaRadioactiva = "";
                    if (technetiu.Checked) substantaRadioactiva = "Technetiu";
                    else if (iod.Checked) substantaRadioactiva = "Iod";
                    else if (Cfluor.Checked) substantaRadioactiva = "Fluor";

                    cmd.Parameters.Add("v_id", OracleDbType.Int32).Value = Convert.ToInt32(idnuclear.Text);
                    cmd.Parameters.Add("v_nume_pacient", OracleDbType.Varchar2).Value = numepacientintrodus.Text;
                    cmd.Parameters.Add("v_substanta_radioactiva", OracleDbType.Varchar2).Value = substantaRadioactiva;
                    cmd.Parameters.Add("v_imagine", OracleDbType.Blob).Value = imageBytes;

                    cmd.ExecuteNonQuery();
                    Ecouu.Text += "Imaginea scintigrafie a fost incarcata cu succes.";
                }
                catch (OracleException ex)
                {
                    Ecouu.Text += " A aparut o eroare in timpul incarcarii imaginii medicale. " + ex.Message;
                }
                catch (Exception ex)
                {
                    Ecouu.Text += " Eroare generala. Te rugam sa selectezi un fisier pentru incarcare. " + ex.Message;
                }
                finally
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            else
            {
                Ecouu.Text = "Fisier imagine lipsa!";
            }
        }

        protected void genereazasemnatura_Click(object sender, EventArgs e)
        {
            Ecouu.Text = "";
            try
            {
                con.Open();
            }
            catch (OracleException ex)
            {
                Ecouu.Text = "Eroare la deschiderea conexiunii: " + ex.Message;
                return;
            }
            OracleCommand cmd = new OracleCommand("genereaza_semnatura", con);
            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                cmd.ExecuteNonQuery();
                Ecouu.Text = "Semnatura imaginii a fost generata cu succes pentru toate inregistrarile.";
            }
            catch (OracleException ex)
            {
                Ecouu.Text = "Eroare la executarea procedurii: " + ex.Message;
            }
            con.Close();
        }

        protected void regasireimagine_Click(object sender, EventArgs e)
        {
            Ecouu.Text = "";
            if (imaginenucleara.HasFile)
            {
                imaginenucleara.SaveAs(@"C:\Users\Miruu\Desktop\proiect baze multi\imagini nucleare\" + imaginenucleara.FileName);

                using (var img = System.IO.File.OpenRead(@"C:\Users\Miruu\Desktop\proiect baze multi\imagini nucleare\" + imaginenucleara.FileName))
                {
                    var imageBytes = new byte[img.Length];
                    img.Read(imageBytes, 0, imageBytes.Length);

                    try
                    {
                        con.Open();
                    }
                    catch (OracleException ex)
                    {
                        Ecouu.Text += "Eroare la deschiderea conexiunii: " + ex.Message;
                        return;
                    }

                    OracleCommand cmd = new OracleCommand("regasireimagine", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("fisier", OracleDbType.Blob).Value = imageBytes;
                    cmd.Parameters.Add("regculoare", OracleDbType.Decimal).Value = Convert.ToDecimal(culoare.Text);
                    cmd.Parameters.Add("regtextura", OracleDbType.Decimal).Value = Convert.ToDecimal(textura.Text);
                    cmd.Parameters.Add("regforma", OracleDbType.Decimal).Value = Convert.ToDecimal(forma.Text);
                    cmd.Parameters.Add("reglocatie", OracleDbType.Decimal).Value = Convert.ToDecimal(locatie.Text);

                    cmd.Parameters.Add("idrez", OracleDbType.Int32).Direction = ParameterDirection.Output;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        Ecouu.Text = "Eroare la execuția procedurii: " + ex.Message;
                        con.Close();
                        return;
                    }

                    idrezultat.Text = cmd.Parameters["idrez"].Value.ToString();
                    idnuclear.Text = idrezultat.Text;

                    con.Close();

                 
                    this.butonafisid_Click(this, e);
                }
            }
            else
            {
                Ecouu.Text = "Fișier imagine lipsă!";
            }
        }

        protected void butonafisid_Click(object sender, EventArgs e)
        {
            Ecouu.Text = "";

            try
            {
                con.Open();

               
                OracleCommand cmd = new OracleCommand("SELECT imagine FROM scintigrafie WHERE id = :id", con);
                cmd.Parameters.Add(":id", OracleDbType.Int32).Value = Convert.ToInt32(idrezultat.Text);

                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
               
                    byte[] imageBytes = (byte[])reader["imagine"];

                    string base64String = Convert.ToBase64String(imageBytes);

                    
                    imagineaa.ImageUrl = "data:image/jpg;base64," + base64String;
                }
                else
                {
                    Ecouu.Text = "Nu s-a găsit imaginea cu acest ID.";
                }

                reader.Close();
                con.Close();
            }
            catch (OracleException ex)
            {
                Ecouu.Text = "Eroare la obținerea imaginii din baza de date: " + ex.Message;
            }
            catch (Exception ex)
            {
                Ecouu.Text = "Eroare generală: " + ex.Message;
            }
        }


    }
}



