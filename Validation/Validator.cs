using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Question2.BLL;
using System.IO;

namespace Question2.Validation
{
    public static class Validator
    {
        public static bool IsValidID(string input)
        {

            int tempID;
            if ((input.Length != 5) || (Int32.TryParse(input, out tempID)))
            {
                MessageBox.Show("Invalid CustomerID, it must be a 5 digit number");
                return false;
            }
            return true;

        }
        public static bool IsValidID(TextBox text)
        {
            int tempID;
            if ((text.TextLength != 5) || !((Int32.TryParse(text.Text, out tempID))))
            {
                MessageBox.Show("Invalid CustomerID, it must be a 5 digit number");
                text.Clear();
                text.Focus();
                return false;
            }
            return true;

        }
        public static bool IsValidName(TextBox text)
        {   
            //Verify if the input is empty
            if(text.Text.Length == 0) 
            { 
                MessageBox.Show("The name should not be empty, please enter again!");
                return false;
            }
            //verify if the input name includes number or whitespace
            else { 
                for (int i = 0; i < text.TextLength; i++)
                {
                    if (char.IsDigit(text.Text, i) || (char.IsWhiteSpace(text.Text, i)))
                    {
                        MessageBox.Show("Invalid Name,Please enter another name.", "INVALID NAME");
                        text.Clear();
                        text.Focus();
                        return false;
                    }

                }
                return true;
             }            
        }
        public static bool IsUniqueID(List<Customer> listC, int id)
        {
            foreach (Customer c in listC)
            {
                if (c.CustomerId == id)
                {
                    MessageBox.Show("Duplicate ID, please enter a unique one.");
                    return false;
                }
            }
            return true;
        }

        public static bool IsValidPhoneNumber(MaskedTextBox text)
        {
            if (text.TextLength != 14)
            {
                MessageBox.Show("Invalid Phone Number, it must be a 10 digit number");
                text.Clear();
                text.Focus();
                return false;
            }
            return true;

        }

        public static bool IsValidDataInFile(string line)
        {
            string[] attributes = {"ID","First Name","Last Name","Phone Number"};
            string[] fields = line.Split(',');

            //Verify if any data in record from DB is empty or null
            for(int i = 0; i < fields.Length; i++)
            {
                if (fields[i] == null || fields[i].Length == 0) 
                {
                    MessageBox.Show("In the DB, there is a record whose " + attributes[i] + "is null or empty. The whole record information is: " + line);
                    return false;
                }
                        
            }
            return true;
        }

    }
}
