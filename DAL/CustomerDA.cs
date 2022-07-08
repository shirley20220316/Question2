using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Question2.BLL;
using System.Windows.Forms;
using Question2.Validation;
using System.IO;

namespace Question2.DAL
{
    // HOME WORK
    //DBA OR QA
    //1 Validation for not allow empty or NULL data at DB or list
    //2 Validation to verify if DB exists Before first Save 

    //STATIC WILL AVOID TO CREATE INS
    public static class CustomerDA
    {
        private static string filePath = Application.StartupPath + @"\Customers.dat";
        private static string fileTemp = Application.StartupPath + @"\Temp.dat";
        public static void Save(Customer cust)
        {   

            StreamReader sReader = new StreamReader(filePath);
                                  
            string line = sReader.ReadLine();

            while (line != null)
            {    /*
                 Although there is a validation for input data from user interface, 
                  there should be another validation for the data set from DB in order to prevent any modification onto file directly.
                 */
                if (!Validator.IsValidDataInFile(line))                //Verify if there is an empty or null value in data set from file 
                {   
                    return;
                }

                string[] fields = line.Split(','); 

                //Verify if the customer ID has already existed in DB
                if (cust.CustomerId == Convert.ToInt32(fields[0]))
                {
                    MessageBox.Show("The customer ID has already existed in database, please user another ID!");
                    return;
                }
                // Verify if customer has already existed in DB by strictly compare its firstname, lastname and phone number with records in DB
                else if (
                    cust.FirstName.Equals(fields[1],StringComparison.OrdinalIgnoreCase) &&
                    cust.LastName.Equals(fields[2],StringComparison.OrdinalIgnoreCase) &&
                    cust.PhoneNumber.Equals(fields[3],StringComparison.OrdinalIgnoreCase)
                    )  
                {
                    MessageBox.Show("The customer has already existed in database, please check again!");
                    return;
                }
                line = sReader.ReadLine(); // Attention : read the next line 
            }
            sReader.Close();//Fixing the Problem by closing the file

            //after verification, the new customer information will save to database
            StreamWriter sWriter = new StreamWriter(filePath, true);
            sWriter.WriteLine(cust.CustomerId + "," + cust.FirstName + "," + cust.LastName + "," + cust.PhoneNumber);
            sWriter.Close();
            MessageBox.Show("Customer Data has been saved.");
            

        }
        public static void ListCustomers(ListView listViewCustomer)
        {
            //step 1: Create an object of type StreamReader
            StreamReader sReader = new StreamReader(filePath);

            listViewCustomer.Items.Clear();
            // Step 2: Read the file until teh end of the file
            //         - Read line by line
            //         - Split the line into an array of string based on seperator
            //         - Add data to the listView control

            //sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

                
            while (line != null)
            {
                if (!Validator.IsValidDataInFile(line))                //Verify if there is an empty or null value in data set from file 
                { 
                  listViewCustomer.Items.Clear();  
                  return;
                }
                string[] fields = line.Split(',');
                ListViewItem item = new ListViewItem(fields[0]);
                item.SubItems.Add(fields[1]);
                item.SubItems.Add(fields[2]);
                item.SubItems.Add(fields[3]);
                listViewCustomer.Items.Add(item);
                line = sReader.ReadLine(); // Attention : read the next line
            }
            sReader.Close();
        }
        public static List<Customer> ListCustomers()
        {
            List<Customer> listC = new List<Customer>();
            //step 1: Create an object of type StreamReader
            StreamReader sReader = new StreamReader(filePath);
            // Step 2: Read the file until teh end of the file
            //         - Read line by line
            //         - Split the line into an array of string based on seperator
            //         - Create an object of type Customer
            //         -Store data in the object Customer
            //         -Add the object to the listC
            //         -Close the file : VERY IMPORTANT

            string line = sReader.ReadLine();
            while (line != null)
            {
                string[] fields = line.Split(',');
                Customer cust = new Customer();
                cust.CustomerId = Convert.ToInt32(fields[0]);
                cust.FirstName = fields[1];
                cust.LastName = fields[2];
                cust.PhoneNumber = fields[3];
                listC.Add(cust);
                line = sReader.ReadLine();
            }
            sReader.Close(); //Close the file
            return listC;
        }
        // Search by different parameters(id, first name, last name)

        public static Customer SearchById(int custId)       //search by customer ID
        {
            Customer cust = new Customer();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if (custId == Convert.ToInt32(fields[0]))
                {
                    cust.CustomerId = Convert.ToInt32(fields[0]);
                    cust.FirstName = fields[1];
                    cust.LastName = fields[2];
                    cust.PhoneNumber = fields[3];
                    sReader.Close();
                    return cust;
                }
                line = sReader.ReadLine(); // Attention : read the next line 
            }
            sReader.Close();//Fixing the Problem by closing the file
            return null;
        }

         public static Customer SearchByFirstName(String fName)       //search by customer's first name
        {
            Customer cust = new Customer();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');  //judge if the input first name is equal to any records in DB
                if (fName.Equals(fields[1],StringComparison.OrdinalIgnoreCase))  //compare two strings with ignoring case
                {
                    cust.CustomerId = Convert.ToInt32(fields[0]);
                    cust.FirstName = fields[1];
                    cust.LastName = fields[2];
                    cust.PhoneNumber = fields[3];
                    sReader.Close();
                    return cust;
                }
                line = sReader.ReadLine(); // Attention : read the next line 
            }
            sReader.Close();//Fixing the Problem by closing the file
            return null;
        }

         public static Customer SearchByLastName(String lName)       //search by customer's last name
        {
            Customer cust = new Customer();

            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');  //judge if the input last name is equal to any records in DB
                if (lName.Equals(fields[2],StringComparison.OrdinalIgnoreCase))  //compare two strings with ignoring case
                {
                    cust.CustomerId = Convert.ToInt32(fields[0]);
                    cust.FirstName = fields[1];
                    cust.LastName = fields[2];
                    cust.PhoneNumber = fields[3];
                    sReader.Close();
                    return cust;
                }
                line = sReader.ReadLine(); // Attention : read the next line 
            }
            sReader.Close();//Fixing the Problem by closing the file
            return null;
        }

        public static void Delete(int custId)
        {
            StreamReader sReader = new StreamReader(filePath);
            string line = sReader.ReadLine();
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((custId) != (Convert.ToInt32(fields[0])))
                {

                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);


                }
                line = sReader.ReadLine(); // Attention : read the next line 
            }
            sReader.Close();
            sWriter.Close();
            //Delete the old file : Customers.dat
            File.Delete(filePath); // Problem here : solved, see the Search method
            File.Move(fileTemp, filePath);

        }
        public static void Update(Customer cust)
        {
            StreamReader sReader = new StreamReader(filePath);
            StreamWriter sWriter = new StreamWriter(fileTemp, true);
            string line = sReader.ReadLine();

            while (line != null)
            {
                string[] fields = line.Split(',');
                if ((Convert.ToInt32(fields[0]) != (cust.CustomerId)))
                {
                    sWriter.WriteLine(fields[0] + "," + fields[1] + "," + fields[2] + "," + fields[3]);
                }

                line = sReader.ReadLine();// Attention : read the next line        
            }
            sWriter.WriteLine(cust.CustomerId + "," + cust.FirstName + "," + cust.LastName + "," + cust.PhoneNumber);
            sReader.Close();
            sWriter.Close();
            File.Delete(filePath);
            File.Move(fileTemp, filePath);


        }

    }
}

