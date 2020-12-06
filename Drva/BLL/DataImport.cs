using Drva.Models.Entities;
using Drva.Models.Import;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;

namespace Drva.BLL
{
    public static class DataImport
    {
        static string ROW_FIRST_NAME = "IME";
        static string ROW_LAST_NAME = "PREZIME";
        static string ROW_STREET = "ULICA";
        static string ROW_PLACE = "MJESTO";
        static string ROW_DISTRICT = "OPĆINA";
        static string ROW_POST_NUMBER = "POŠTANSKI BR.";
        static string ROW_PHONE_NUMBER = "TEL FIKSNI";
        static string ROW_WOOD_TYPE = "DRVO";
        static string ROW_SAW_TYPE = "MJERA";
        static string ROW_AMOUNT = "KOLIČINA 1";
        static string ROW_PRICE = "JED.CIJENA";
        static string ROW_DESCRIPTION = "NAPOMENA";
        public static void ImportData()
        {
            string file = @"C:\Users\Jan\Desktop\Podaci.xlsx";
            Excel.Application excel = null;
            Excel.Workbook wkb = null;
            try
            {
                excel = new Excel.Application();
                wkb = OpenBook(excel, file, true, false, false);
                List<Customer> customers = new List<Customer>();
                List<ImportUnit> firstSheet = new List<ImportUnit>();
                List<ImportUnit> secondSheet = new List<ImportUnit>();
                DateTime startingDate = new DateTime(2013, 12, 7);
                int sheetCounter = 0;

                foreach (Excel.Worksheet sheet in wkb.Sheets)
                {
                    DateTime currentDate = startingDate.AddDays(sheetCounter * 7);
                    bool flagFirstRow = true;
                    int columnNumberFirstName = 0;
                    int columnNumberLasttName = 0;
                    int columnNumberStreet = 0;
                    int columnNumberPlace = 0;
                    int columnNumberDistrict = 0;
                    int columnNumberPostNumber = 0;
                    int columnNumberPhoneNumber = 0;
                    int columnNumberWoodType = 0;
                    int columnNumberSawType = 0;
                    int columnNumberAmount = 0;
                    int columnNumberPrice = 0;
                    int columnNumberDescription = 0;
                    foreach (Excel.Range row in sheet.Rows)
                    {
                        ImportUnit unit = new ImportUnit();                 
                        int counter = 0;                        
                        bool flagFirstCell = true;
                        bool killSheet = false;
                        foreach (Excel.Range cell in row.Cells)
                        {
                            String text = cell.Text.ToString();
                            text = text.Trim();

                            // If it is the first cell in the row and it is empty we are over with the sheet
                            if (text.Equals("") && flagFirstCell)
                            {
                                killSheet = true;
                                break;
                            }
                            if (text.Equals("") && counter > 18)
                            {
                                break;
                            }

                            // Figure out in every sheet by the first row in which column is which data
                            // and apply appropriate columnt index
                            if (flagFirstRow)
                            {
                                if (text.Equals(ROW_FIRST_NAME))
                                {
                                    columnNumberFirstName = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                if (text.Equals(ROW_LAST_NAME))
                                {
                                    columnNumberLasttName = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                if (text.Equals(ROW_DESCRIPTION))
                                {
                                    columnNumberDescription = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                if (text.Equals(ROW_STREET))
                                {
                                    columnNumberStreet = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                if (text.Equals(ROW_PRICE))
                                {
                                    columnNumberPrice = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                if (text.Equals(ROW_AMOUNT) && columnNumberAmount == 0)
                                {
                                    columnNumberAmount = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                if (text.Equals(ROW_WOOD_TYPE))
                                {
                                    columnNumberWoodType = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                if (text.Equals(ROW_SAW_TYPE))
                                {
                                    columnNumberSawType = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                if (text.Equals(ROW_PHONE_NUMBER))
                                {
                                    columnNumberPhoneNumber = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                if (text.Equals(ROW_POST_NUMBER))
                                {
                                    columnNumberPostNumber = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                if (text.Equals(ROW_DISTRICT))
                                {
                                    columnNumberDistrict = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                if (text.Equals(ROW_PLACE))
                                {
                                    columnNumberPlace = counter;
                                    counter++;
                                    flagFirstCell = false;
                                    continue;
                                }
                                counter++;
                                flagFirstCell = false;
                                continue;
                            }
                            // Setting up import unit data
                            if (counter == columnNumberFirstName)
                            {
                                unit.FirstName = text;
                                flagFirstCell = false;
                            }
                            if (counter == columnNumberLasttName)
                            {
                                unit.LastName = text;
                                flagFirstCell = false;
                            }
                            if (counter == columnNumberDescription)
                            {
                                unit.Description = text;
                                flagFirstCell = false;
                            }
                            if (counter == columnNumberPhoneNumber)
                            {
                                unit.PhoneNumber = text;
                                flagFirstCell = false;
                            }
                            if (counter == columnNumberPlace)
                            {
                                unit.Place = text;
                                flagFirstCell = false;
                            }
                            if (counter == columnNumberPostNumber)
                            {
                                unit.PostNumber = text;
                                flagFirstCell = false;
                            }
                            if (counter == columnNumberStreet)
                            {
                                unit.StreetAndNumber = text;
                                flagFirstCell = false;
                            }
                            if (counter == columnNumberDistrict)
                            {
                                unit.District = text;
                                flagFirstCell = false;
                            }
                            if (counter == columnNumberAmount)
                            {
                                unit.Amount = text;
                                flagFirstCell = false;
                            }
                            if (counter == columnNumberPrice)
                            {
                                unit.Price = text;
                                flagFirstCell = false;
                            }
                            if (counter == columnNumberSawType)
                            {
                                unit.SawingType = text;
                                flagFirstCell = false;
                            }
                            if (counter == columnNumberWoodType)
                            {
                                unit.WoodType = text;
                                flagFirstCell = false;
                            }
                            flagFirstCell = false;
                            counter++;
                        }
                        if (killSheet)
                        {
                            break;
                        }
                        if (flagFirstRow)
                        {
                            flagFirstRow = false;
                            continue;
                        }
                        secondSheet.Add(unit);
                    }
                    if (sheetCounter != 0)
                    {
                        // Comparing two sheets to see what was delivered from the first sheet
                        foreach (ImportUnit secondSheetUnit in secondSheet)
                        {
                            for (int i = 0; i < firstSheet.Count; i++)
                            {
                                ImportUnit firstSheetUnit = firstSheet[i];
                                if (secondSheetUnit.FirstName.Equals(firstSheetUnit.FirstName) &&
                                        secondSheetUnit.LastName.Equals(firstSheetUnit.LastName) &&
                                        secondSheetUnit.StreetAndNumber.Equals(firstSheetUnit.StreetAndNumber) &&
                                        secondSheetUnit.Place.Equals(firstSheetUnit.Place) &&
                                        secondSheetUnit.District.Equals(firstSheetUnit.District) &&
                                        secondSheetUnit.PostNumber.Equals(firstSheetUnit.PostNumber))
                                {
                                    firstSheet.Remove(firstSheetUnit);
                                    break;
                                }
                            }                           
                        }

                        // Add new units
                        foreach (ImportUnit newUnit in firstSheet)
                        {
                            bool isNewUnit = true;
                            int oldCustomerPosition = 0;
                            for (int i = 0; i < customers.Count; i++)
                            {
                                Customer oldCustomer = customers[i];
                                if (oldCustomer.FirstName.Equals(newUnit.FirstName) &&
                                            oldCustomer.LastName.Equals(newUnit.LastName) &&
                                            oldCustomer.Address.StreetAndNumber.Equals(newUnit.StreetAndNumber) &&
                                            oldCustomer.Address.Place.Equals(newUnit.Place) &&
                                            oldCustomer.Address.District.Equals(newUnit.District) &&
                                            oldCustomer.Address.PostNumber.Equals(newUnit.PostNumber))
                                {
                                    isNewUnit = false;
                                    oldCustomerPosition = i;
                                    break;
                                }
                            }

                            // Create a new customer from the unit
                            if (isNewUnit)
                            {
                                Customer customer = new Customer();
                                customer.FirstName = newUnit.FirstName;
                                customer.LastName = newUnit.LastName;
                                customer.Description = newUnit.Description;

                                customer.Address = new Address();
                                customer.Address.StreetAndNumber = newUnit.StreetAndNumber;
                                customer.Address.Place = newUnit.Place;
                                customer.Address.PostNumber = newUnit.PostNumber;
                                customer.Address.District = newUnit.District;

                                customer.PhoneNumbers = new List<PhoneNumber>();

                                if (!newUnit.PhoneNumber.Equals(""))
                                {
                                    customer.PhoneNumbers.Add(new PhoneNumber
                                    {
                                        Number = newUnit.PhoneNumber
                                    });
                                }

                                Unit unit = new Unit();
                                unit.WoodType = newUnit.WoodType;
                                unit.SawingType = newUnit.SawingType;
                                unit.Amount = newUnit.Amount;
                                unit.Price = newUnit.Price;

                                Order order = new Order();
                                order.DeliveryDate = currentDate;
                                order.Units = new List<Unit>();
                                order.Units.Add(unit);

                                customer.Orders = new List<Order>();                                
                                customer.Orders.Add(order);

                                customers.Add(customer);
                            }
                            else
                            {
                                // Enter new unit to the old customer either as a new unit in an order or a new order.
                                // Figure that out by the delivery date.
                                Customer oldCustomer = customers[oldCustomerPosition];

                                if (!newUnit.PhoneNumber.Equals(""))
                                {
                                    bool isNewPhoneNumber = true;
                                    foreach (PhoneNumber phoneNumber in oldCustomer.PhoneNumbers)
                                    {
                                        if (phoneNumber.Number.Equals(newUnit.PhoneNumber))
                                        {
                                            isNewPhoneNumber = false;
                                        }
                                    }
                                    if (isNewPhoneNumber)
                                    {
                                        oldCustomer.PhoneNumbers.Add(new PhoneNumber
                                        {
                                            Number = newUnit.PhoneNumber
                                        });
                                    }
                                }

                                bool enteredAsUnit = false;
                                for (int i = 0; i < oldCustomer.Orders.Count; i++)
                                {
                                    Order order = oldCustomer.Orders.ElementAt(i);
                                    if (order.DeliveryDate.Equals(currentDate))
                                    {
                                        enteredAsUnit = true;

                                        Unit unit = new Unit();
                                        unit.WoodType = newUnit.WoodType;
                                        unit.SawingType = newUnit.SawingType;
                                        unit.Amount = newUnit.Amount;
                                        unit.Price = newUnit.Price;
                                        order.Units.Add(unit);
                                        break;
                                    }
                                }
                                if (!enteredAsUnit)
                                {
                                    Unit unit = new Unit();
                                    unit.WoodType = newUnit.WoodType;
                                    unit.SawingType = newUnit.SawingType;
                                    unit.Amount = newUnit.Amount;
                                    unit.Price = newUnit.Price;

                                    Order order = new Order();
                                    order.DeliveryDate = currentDate;
                                    order.Units = new List<Unit>();
                                    order.Units.Add(unit);

                                    oldCustomer.Orders.Add(order);
                                }
                            }
                        }
                    }
                    firstSheet = secondSheet;
                    secondSheet = new List<ImportUnit>();
                    sheetCounter++;
                }

                foreach (ImportUnit newUnit in firstSheet)
                {
                    bool isNewUnit = true;
                    int oldCustomerPosition = 0;
                    for (int i = 0; i < customers.Count; i++)
                    {
                        Customer oldCustomer = customers[i];
                        if (oldCustomer.FirstName.Equals(newUnit.FirstName) &&
                                    oldCustomer.LastName.Equals(newUnit.LastName) &&
                                    oldCustomer.Address.StreetAndNumber.Equals(newUnit.StreetAndNumber) &&
                                    oldCustomer.Address.Place.Equals(newUnit.Place) &&
                                    oldCustomer.Address.District.Equals(newUnit.District) &&
                                    oldCustomer.Address.PostNumber.Equals(newUnit.PostNumber))
                        {
                            isNewUnit = false;
                            oldCustomerPosition = i;
                            break;
                        }
                    }

                    // Create a new customer from the unit
                    if (isNewUnit)
                    {
                        Customer customer = new Customer();
                        customer.FirstName = newUnit.FirstName;
                        customer.LastName = newUnit.LastName;
                        customer.Description = newUnit.Description;

                        customer.Address = new Address();
                        customer.Address.StreetAndNumber = newUnit.StreetAndNumber;
                        customer.Address.Place = newUnit.Place;
                        customer.Address.PostNumber = newUnit.PostNumber;
                        customer.Address.District = newUnit.District;

                        customer.PhoneNumbers = new List<PhoneNumber>();

                        if (!newUnit.PhoneNumber.Equals(""))
                        {
                            customer.PhoneNumbers.Add(new PhoneNumber
                            {
                                Number = newUnit.PhoneNumber
                            });
                        }

                        Unit unit = new Unit();
                        unit.WoodType = newUnit.WoodType;
                        unit.SawingType = newUnit.SawingType;
                        unit.Amount = newUnit.Amount;
                        unit.Price = newUnit.Price;

                        Order order = new Order();
                        order.Units = new List<Unit>();
                        order.Units.Add(unit);

                        customer.Orders = new List<Order>();
                        customer.Orders.Add(order);

                        customers.Add(customer);
                    }
                    else
                    {
                        // Enter new unit to the old customer either as a new unit in an order or a new order.
                        // Figure that out by the delivery date.
                        Customer oldCustomer = customers[oldCustomerPosition];

                        if (!newUnit.PhoneNumber.Equals(""))
                        {
                            bool isNewPhoneNumber = true;
                            foreach (PhoneNumber phoneNumber in oldCustomer.PhoneNumbers)
                            {
                                if (phoneNumber.Number.Equals(newUnit.PhoneNumber))
                                {
                                    isNewPhoneNumber = false;
                                }
                            }
                            if (isNewPhoneNumber)
                            {
                                oldCustomer.PhoneNumbers.Add(new PhoneNumber
                                {
                                    Number = newUnit.PhoneNumber
                                });
                            }
                        }

                        bool enteredAsUnit = false;
                        for (int i = 0; i < oldCustomer.Orders.Count; i++)
                        {
                            Order order = oldCustomer.Orders.ElementAt(i);
                            if (order.DeliveryDate.Equals(null))
                            {
                                enteredAsUnit = true;

                                Unit unit = new Unit();
                                unit.WoodType = newUnit.WoodType;
                                unit.SawingType = newUnit.SawingType;
                                unit.Amount = newUnit.Amount;
                                unit.Price = newUnit.Price;
                                order.Units.Add(unit);
                                break;
                            }
                        }
                        if (!enteredAsUnit)
                        {
                            Unit unit = new Unit();
                            unit.WoodType = newUnit.WoodType;
                            unit.SawingType = newUnit.SawingType;
                            unit.Amount = newUnit.Amount;
                            unit.Price = newUnit.Price;

                            Order order = new Order();
                            order.Units = new List<Unit>();
                            order.Units.Add(unit);

                            oldCustomer.Orders.Add(order);
                        }
                    }
                }
                DatabaseContext db = new DatabaseContext();
                db.Customers.AddRange(customers);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (wkb != null)
                    ReleaseRCM(wkb);

                if (excel != null)
                    ReleaseRCM(excel);
            }
        }

        public static Excel.Workbook OpenBook(Excel.Application excelInstance, string fileName, bool readOnly, bool editable,
        bool updateLinks)
        {
            Excel.Workbook book = excelInstance.Workbooks.Open(
                fileName, updateLinks, readOnly,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, editable, Type.Missing, Type.Missing, Type.Missing,
                Type.Missing, Type.Missing);
            return book;
        }

        public static void ReleaseRCM(object o)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(o);
            }
            catch
            {
            }
            finally
            {
                o = null;
            }
        }
    }
}