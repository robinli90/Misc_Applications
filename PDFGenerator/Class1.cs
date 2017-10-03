using Databases;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace ClassLibrary1
    {

        internal class PDFGenerator
        {
            private iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, 0, BaseColor.BLACK);
            private iTextSharp.text.Font _standardFontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, 1, BaseColor.BLACK);
            private iTextSharp.text.Font _titleFontBold = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 20f, 1, BaseColor.BLACK);
            private string[] cust_info = new string[0x12];
            private Document doc1 = new Document();
            private List<string> extra_charge_info = new List<string>();
            private string folder_path = @"S:\Development\Robin\Apps\Data_Files\Invoices\";
            private int line_count;
            private List<string>[] order_info = new List<string>[10];
            private string ordernumber = "";
            private int total_extra_lines = -1;
            private int total_lines;

            public void Generate_Invoice(string _ordernumber)
            {
                PdfPCell cell;
                this.Reset_Parameters();
                this.ordernumber = _ordernumber;
                this.Get_Customer_Info(this.ordernumber);
                this.Get_Order_Info(this.ordernumber);
                this.Get_Extra_Charges(this.ordernumber);
                PdfWriter.GetInstance(this.doc1, new FileStream(this.folder_path + this.ordernumber + "_INVOICE.pdf", FileMode.Create));
                this.doc1.Open();
                this.doc1.Add(new Paragraph("                                                         INVOICE", this._titleFontBold));
                string str = " insert into Exco_File_System_Record values ('" + this.ordernumber + "', '" + DateTime.Now.ToString() + "', '99999', 'Invoices', '" + this.ordernumber + "_INVOICES.pdf', '" + this.folder_path + this.ordernumber + "_INVOICE.pdf', 'Automated Generated Invoice')";
                Console.WriteLine(str);
                ExcoODBC instance = ExcoODBC.Instance;
                instance.Open(Database.DECADE_MARKHAM);
                instance.RunQuery(str, 10).Close();
                this.doc1.SetMargins(50f, 50f, 50f, 50f);
                this.doc1.SetPageSize(new Rectangle(PageSize.LETTER.Width, PageSize.LETTER.Height));
                this.doc1.AddCreator("Exco");
                this.doc1.AddKeywords("");
                PdfPTable element = new PdfPTable(1)
                {
                    TotalWidth = 500f,
                    LockedWidth = true,
                    HorizontalAlignment = 0,
                    SpacingBefore = 20f,
                    SpacingAfter = 30f
                };
                PdfPTable table = new PdfPTable(6)
                {
                    LockedWidth = false,
                    DefaultCell = { Border = 0 }
                };
                float[] relativeWidths = new float[] { 1f, 1f, 1f, 1f, 1f, 1f };
                table.SetWidths(relativeWidths);
                table.HorizontalAlignment = 0;
                for (int i = 0; i < 6; i++)
                {
                    cell = new PdfPCell(new Phrase("", this._standardFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                        HorizontalAlignment = 1
                    };
                    table.AddCell(cell);
                }
                for (int j = 0; j < 5; j++)
                {
                    cell = new PdfPCell(new Phrase("", this._standardFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                        HorizontalAlignment = 1
                    };
                    table.AddCell(cell);
                }
                cell = new PdfPCell(new Phrase("INVOICE DATE", this._standardFontBold))
                {
                    BackgroundColor = new BaseColor(0xc0, 0xc0, 0xc0),
                    HorizontalAlignment = 1
                };
                table.AddCell(cell);
                for (int k = 0; k < 5; k++)
                {
                    cell = new PdfPCell(new Phrase("", this._standardFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                        HorizontalAlignment = 1
                    };
                    table.AddCell(cell);
                }
                cell = new PdfPCell(new Phrase(this.cust_info[0x10], this._standardFont))
                {
                    BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                    HorizontalAlignment = 1
                };
                table.AddCell(cell);
                for (int m = 0; m < 5; m++)
                {
                    cell = new PdfPCell(new Phrase("", this._standardFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                        HorizontalAlignment = 1
                    };
                    table.AddCell(cell);
                }
                cell = new PdfPCell(new Phrase("SHIP DATE", this._standardFontBold))
                {
                    BackgroundColor = new BaseColor(0xc0, 0xc0, 0xc0),
                    HorizontalAlignment = 1
                };
                table.AddCell(cell);
                for (int n = 0; n < 5; n++)
                {
                    cell = new PdfPCell(new Phrase("", this._standardFont))
                    {
                        Border = 0,
                        BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                        HorizontalAlignment = 1
                    };
                    table.AddCell(cell);
                }
                cell = new PdfPCell(new Phrase(this.cust_info[0x11], this._standardFont))
                {
                    BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                    HorizontalAlignment = 1
                };
                table.AddCell(cell);
                PdfPCell cell2 = new PdfPCell(table)
                {
                    Border = 0
                };
                element.AddCell(cell2);
                table = new PdfPTable(6)
                {
                    LockedWidth = false,
                    DefaultCell = { Border = 0 }
                };
                float[] numArray2 = new float[] { 4f, 13f, 5f, 4f, 13f, 5f };
                table.SetWidths(numArray2);
                table.HorizontalAlignment = 0;
                table.AddCell(this.return_generated_column(new string[] { "Sold To:", "", "", "", "", "" }, 7, 1));
                table.AddCell(this.return_generated_column(new string[] { this.cust_info[1], this.cust_info[2], this.cust_info[3], this.cust_info[4], this.cust_info[5], "" }, 7, 0));
                table.AddCell(this.return_generated_column(new string[] { "", "", "", "", "", this.cust_info[6] }, 7, 1));
                table.AddCell(this.return_generated_column(new string[] { "Ship To:", "", "", "", "", "" }, 7, 1));
                table.AddCell(this.return_generated_column(new string[] { this.cust_info[1], this.cust_info[7], this.cust_info[8], this.cust_info[9], this.cust_info[10], "" }, 7, 0));
                table.AddCell(this.return_generated_column(new string[] { "", "", "", "", "", this.cust_info[11] }, 7, 1));
                cell2 = new PdfPCell(table)
                {
                    Border = 0
                };
                element.AddCell(cell2);
                PdfPTable table3 = new PdfPTable(3)
                {
                    LockedWidth = false
                };
                float[] numArray3 = new float[] { 1f, 1f, 1f };
                table3.SetWidths(numArray3);
                table3.HorizontalAlignment = 0;
                cell = new PdfPCell(new Phrase("PACKING SLIP", this._standardFontBold))
                {
                    BackgroundColor = new BaseColor(0xc0, 0xc0, 0xc0),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase("PURCHASE ORDER", this._standardFontBold))
                {
                    BackgroundColor = new BaseColor(0xc0, 0xc0, 0xc0),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase("TERMS", this._standardFontBold))
                {
                    BackgroundColor = new BaseColor(0xc0, 0xc0, 0xc0),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase(this.ordernumber, this._standardFont))
                {
                    BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase(this.cust_info[15], this._standardFont))
                {
                    BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase(this.cust_info[12], this._standardFont))
                {
                    BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell2 = new PdfPCell(table3)
                {
                    Border = 0
                };
                element.AddCell(cell2);
                table3 = new PdfPTable(5)
                {
                    LockedWidth = false
                };
                float[] numArray4 = new float[] { 4f, 8f, 1f, 3f, 3f };
                table3.SetWidths(numArray4);
                table3.HorizontalAlignment = 0;
                cell = new PdfPCell(new Phrase("ITEM NUMBER", this._standardFontBold))
                {
                    BackgroundColor = new BaseColor(0xc0, 0xc0, 0xc0),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase("DESCRIPTION", this._standardFontBold))
                {
                    BackgroundColor = new BaseColor(0xc0, 0xc0, 0xc0),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase("QTY", this._standardFontBold))
                {
                    BackgroundColor = new BaseColor(0xc0, 0xc0, 0xc0),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase("UNIT PRICE", this._standardFontBold))
                {
                    BackgroundColor = new BaseColor(0xc0, 0xc0, 0xc0),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase("AMOUNT", this._standardFontBold))
                {
                    BackgroundColor = new BaseColor(0xc0, 0xc0, 0xc0),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                for (int num6 = 0; num6 < this.line_count; num6++)
                {
                    int index = 0;
                    int num8 = 0;
                    int num9 = 0;
                    int num10 = 0;
                    string[] strArray = new string[10];
                    string[] strArray2 = new string[10];
                    string[] strArray3 = new string[10];
                    string[] strArray4 = new string[10];
                    int lines = this.order_info[num6].Count / 4;
                    table3.AddCell(this.return_generated_column(new string[] { this.order_info[num6][0], "", "", "" }, lines, 1));
                    for (int num12 = 1; num12 < this.order_info[num6].Count<string>(); num12++)
                    {
                        if ((num12 % 4) == 1)
                        {
                            strArray[index] = this.order_info[num6][num12];
                            index++;
                        }
                        if ((num12 % 4) == 2)
                        {
                            strArray2[num8] = this.order_info[num6][num12];
                            num8++;
                        }
                        if ((num12 % 4) == 3)
                        {
                            strArray3[num9] = this.order_info[num6][num12];
                            num9++;
                        }
                        if ((num12 % 4) == 0)
                        {
                            strArray4[num10] = this.order_info[num6][num12];
                            num10++;
                        }
                    }
                    table3.AddCell(this.return_generated_column(strArray, lines, 0));
                    table3.AddCell(this.return_generated_column(strArray2, lines, 1));
                    table3.AddCell(this.return_generated_column(strArray3, lines, 2));
                    table3.AddCell(this.return_generated_column(strArray4, lines, 2));
                    this.total_lines += lines;
                }
                table3.AddCell(this.return_generated_column(new string[0], (0x21 - this.total_lines) - this.total_extra_lines, 1));
                table3.AddCell(this.return_generated_column(new string[0], (0x21 - this.total_lines) - this.total_extra_lines, 1));
                table3.AddCell(this.return_generated_column(new string[0], (0x21 - this.total_lines) - this.total_extra_lines, 1));
                table3.AddCell(this.return_generated_column(new string[0], (0x21 - this.total_lines) - this.total_extra_lines, 1));
                table3.AddCell(this.return_generated_column(new string[0], (0x21 - this.total_lines) - this.total_extra_lines, 1));
                table3.AddCell(this.return_generated_column(new string[] { "", "" }, this.total_extra_lines, 1));
                table3.AddCell(this.return_generated_column(new string[] { "", "" }, this.total_extra_lines, 1));
                table3.AddCell(this.return_generated_column(new string[] { "", "" }, this.total_extra_lines, 1));
                string[] entries = new string[5];
                string[] strArray6 = new string[5];
                entries[0] = "SUB TOTAL";
                strArray6[0] = "$" + this.extra_charge_info[5];
                if (Convert.ToDouble(this.extra_charge_info[0]) > 0.0)
                {
                    entries[1] = "FREIGHT";
                    strArray6[1] = "$" + this.extra_charge_info[0];
                }
                if (Convert.ToDouble(this.extra_charge_info[1]) > 0.0)
                {
                    entries[2] = "HST";
                    strArray6[2] = "$" + this.extra_charge_info[1];
                }
                if (Convert.ToDouble(this.extra_charge_info[2]) > 0.0)
                {
                    entries[3] = "FAST TRACK";
                    strArray6[3] = "$" + this.extra_charge_info[2];
                }
                if (Convert.ToDouble(this.extra_charge_info[3]) > 0.0)
                {
                    entries[4] = "DISCOUNT";
                    strArray6[4] = "$" + this.extra_charge_info[3];
                }
                for (int num13 = 0; num13 < 5; num13++)
                {
                    if (entries[num13] == null)
                    {
                        try
                        {
                            entries[num13] = entries[num13 + 1];
                            entries[num13 + 1] = null;
                            strArray6[num13] = strArray6[num13 + 1];
                            strArray6[num13 + 1] = null;
                        }
                        catch
                        {
                        }
                    }
                }
                table3.AddCell(this.return_generated_column(entries, this.total_extra_lines, 1));
                table3.AddCell(this.return_generated_column(strArray6, this.total_extra_lines, 2));
                cell2 = new PdfPCell(table3)
                {
                    Border = 0
                };
                element.AddCell(cell2);
                table3 = new PdfPTable(3)
                {
                    LockedWidth = false
                };
                float[] numArray5 = new float[] { 13f, 15f, 14f };
                table3.SetWidths(numArray5);
                table3.HorizontalAlignment = 0;
                cell = new PdfPCell(new Phrase("CUSTOMER NO.: " + this.cust_info[0], this._standardFont))
                {
                    BackgroundColor = new BaseColor(210, 210, 210),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase("HST#: 101714533", this._standardFont))
                {
                    BackgroundColor = new BaseColor(210, 210, 210),
                    HorizontalAlignment = 1
                };
                table3.AddCell(cell);
                cell = new PdfPCell(new Phrase("(" + this.cust_info[13] + ") $" + this.extra_charge_info[4], this._standardFontBold))
                {
                    BackgroundColor = new BaseColor(210, 210, 210),
                    HorizontalAlignment = 2
                };
                table3.AddCell(cell);
                cell2 = new PdfPCell(table3)
                {
                    Border = 0
                };
                element.AddCell(cell2);
                this.doc1.Add(element);
                this.doc1.Close();
            }

            private void Get_Customer_Info(string soNum)
            {
                ExcoODBC instance = ExcoODBC.Instance;
                string query = "select customercode, baddress1, baddress2, baddress3, baddress4, bpostalcode, saddress1, saddress2, saddress3, saddress4, spostalcode, customerpo, invoicedate, shipdate from d_order where ordernumber = '" + soNum + "'";
                instance.Open(Database.DECADE_MARKHAM);
                OdbcDataReader reader = instance.RunQuery(query, 10);
                reader.Read();
                this.cust_info[0] = reader[0].ToString().Trim();
                this.cust_info[2] = reader[1].ToString().Trim();
                this.cust_info[3] = reader[2].ToString().Trim();
                this.cust_info[4] = reader[3].ToString().Trim();
                this.cust_info[5] = reader[4].ToString().Trim();
                this.cust_info[6] = reader[5].ToString().Trim();
                this.cust_info[7] = reader[6].ToString().Trim();
                this.cust_info[8] = reader[7].ToString().Trim();
                this.cust_info[9] = reader[8].ToString().Trim();
                this.cust_info[10] = reader[9].ToString().Trim();
                this.cust_info[11] = reader[10].ToString().Trim();
                this.cust_info[15] = reader[11].ToString().Trim();
                if (this.cust_info[15].Length < 1)
                {
                    this.cust_info[15] = " ";
                }
                this.cust_info[0x10] = reader[12].ToString().Trim();
                if (this.cust_info[0x10].Length < 1)
                {
                    this.cust_info[0x10] = " ";
                }
                this.cust_info[0x11] = reader[13].ToString().Trim();
                if (this.cust_info[0x11].Length < 1)
                {
                    this.cust_info[0x11] = " ";
                }
                reader.Close();
                query = "select name, accountset, terms from d_customer where customercode = '" + this.cust_info[0] + "'";
                instance.Open(Database.DECADE_MARKHAM);
                reader = instance.RunQuery(query, 10);
                reader.Read();
                this.cust_info[1] = reader[0].ToString().Trim();
                this.cust_info[13] = reader[1].ToString().Trim();
                this.cust_info[12] = reader[2].ToString().Trim();
                reader.Close();
            }

            private void Get_Extra_Charges(string soNum)
            {
                ExcoODBC instance = ExcoODBC.Instance;
                string query = "select freight as Freight, CONVERT(varchar, cast(total as money), 1) as Total, gst as GST, fasttrackcharge as FastTrack, discountamount as Discount, CONVERT(varchar, cast(total-gst-freight-fasttrack+discountamount as money), 1) as subtotal from d_order where ordernumber = '" + soNum + "'";
                instance.Open(Database.DECADE_MARKHAM);
                OdbcDataReader reader = instance.RunQuery(query, 10);
                reader.Read();
                this.extra_charge_info.Add(reader[0].ToString().Trim());
                this.extra_charge_info.Add(reader[2].ToString().Trim());
                this.extra_charge_info.Add(reader[3].ToString().Trim());
                this.extra_charge_info.Add(reader[4].ToString().Trim());
                this.extra_charge_info.Add(reader[1].ToString().Trim());
                this.extra_charge_info.Add(reader[5].ToString().Trim());
                foreach (string str2 in this.extra_charge_info)
                {
                    if (Convert.ToDouble(str2) > 0.0)
                    {
                        this.total_extra_lines++;
                    }
                }
                this.total_lines += this.total_extra_lines;
                reader.Close();
            }

            private void Get_Order_Info(string soNum)
            {
                List<string> collection = new List<string>();
                ExcoODBC instance = ExcoODBC.Instance;
                string query = "select line, qty, description, CONVERT(varchar, cast(baseprice as money), 1),  CONVERT(varchar, cast(price as money), 1), dienumber as die#, steelcost as surcharge, location, note  from d_orderitem where ordernumber = '" + soNum + "' order by line asc";
                instance.Open(Database.DECADE_MARKHAM);
                OdbcDataReader reader = instance.RunQuery(query, 10);
                while (reader.Read())
                {
                    this.line_count++;
                    collection = new List<string> {
                    reader[5].ToString().Trim(),
                    reader[2].ToString().Trim(),
                    reader[1].ToString().Trim(),
                    "$" + reader[3].ToString().Trim(),
                    "$" + reader[4].ToString().Trim()
                };
                    if (reader[7].ToString().Trim().Length > 0)
                    {
                        collection.Add("    LOCATION: " + reader[7].ToString().Trim());
                        collection.Add("");
                        collection.Add("");
                        collection.Add("");
                    }
                    if (reader[8].ToString().Trim().Length > 0)
                    {
                        collection.Add("    NOTE: " + reader[8].ToString().Trim());
                        collection.Add("");
                        collection.Add("");
                        collection.Add("");
                    }
                    if (Convert.ToDouble(reader[6].ToString().Trim()) > 0.0)
                    {
                        collection.Add("        STEEL SURCHARGE");
                        collection.Add("");
                        collection.Add("");
                        collection.Add("$" + reader[6].ToString().Trim());
                    }
                    reader[0].ToString().Trim();
                    this.order_info[Convert.ToInt32(reader[0].ToString().Trim()) - 1] = collection;
                }
                query = "select line, chargename, CONVERT(varchar, cast(price as money), 1), qty,  CONVERT(varchar, cast(chargeprice as money), 1) from d_orderitemcharges where ordernumber = '" + soNum + "' order by line asc";
                instance.Open(Database.DECADE_MARKHAM);
                reader = instance.RunQuery(query, 10);
                while (reader.Read())
                {
                    collection = new List<string> {
                    "        " + reader[1].ToString().Trim(),
                    reader[3].ToString().Trim(),
                    "$" + reader[4].ToString().Trim(),
                    "$" + reader[2].ToString().Trim()
                };
                    this.order_info[Convert.ToInt32(reader[0].ToString().Trim()) - 1].AddRange(collection);
                }
                reader.Close();
            }

            private void Reset_Parameters()
            {
                this.doc1 = new Document();
                this.order_info = new List<string>[10];
                this.extra_charge_info = new List<string>();
                this.cust_info = new string[0x12];
                this.line_count = 0;
                this.total_lines = 0;
                this.total_extra_lines = -1;
            }

            public PdfPTable return_generated_column(string[] entries, int lines, int orientation = 1)
            {
                PdfPTable table = new PdfPTable(1)
                {
                    LockedWidth = false,
                    HorizontalAlignment = 0
                };
                for (int i = 0; i < lines; i++)
                {
                    PdfPCell cell;
                    try
                    {
                        if (entries[i] != "")
                        {
                            if (orientation == 1)
                            {
                                cell = new PdfPCell(new Phrase(entries[i], this._standardFont))
                                {
                                    Border = 0,
                                    BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                                    HorizontalAlignment = 1
                                };
                                table.AddCell(cell);
                            }
                            else if (orientation == 0)
                            {
                                cell = new PdfPCell(new Phrase(entries[i], this._standardFont))
                                {
                                    Border = 0,
                                    BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                                    HorizontalAlignment = 0
                                };
                                table.AddCell(cell);
                            }
                            else
                            {
                                cell = new PdfPCell(new Phrase(entries[i], this._standardFont))
                                {
                                    Border = 0,
                                    BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                                    HorizontalAlignment = 2
                                };
                                table.AddCell(cell);
                            }
                        }
                        else if (orientation == 1)
                        {
                            cell = new PdfPCell(new Phrase(" ", this._standardFont))
                            {
                                Border = 0,
                                BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                                HorizontalAlignment = 1
                            };
                            table.AddCell(cell);
                        }
                        else if (orientation == 0)
                        {
                            cell = new PdfPCell(new Phrase(" ", this._standardFont))
                            {
                                Border = 0,
                                BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                                HorizontalAlignment = 0
                            };
                            table.AddCell(cell);
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase(" ", this._standardFont))
                            {
                                Border = 0,
                                BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                                HorizontalAlignment = 2
                            };
                            table.AddCell(cell);
                        }
                    }
                    catch
                    {
                        if (orientation == 1)
                        {
                            cell = new PdfPCell(new Phrase(" ", this._standardFont))
                            {
                                Border = 0,
                                BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                                HorizontalAlignment = 1
                            };
                            table.AddCell(cell);
                        }
                        else if (orientation == 0)
                        {
                            cell = new PdfPCell(new Phrase(" ", this._standardFont))
                            {
                                Border = 0,
                                BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                                HorizontalAlignment = 0
                            };
                            table.AddCell(cell);
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase(" ", this._standardFont))
                            {
                                Border = 0,
                                BackgroundColor = new BaseColor(0xff, 0xff, 0xff),
                                HorizontalAlignment = 2
                            };
                            table.AddCell(cell);
                        }
                    }
                }
                return table;
            }
        }
    }

