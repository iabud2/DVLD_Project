using DVLD_BusinessLayer;
using DVLD_BusinessLayer.Drivers;
using DVLD_BusinessLayer.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_DVLD_.Controls
{
    public partial class ucShowLicenseInfo : UserControl
    {
        public ucShowLicenseInfo()
        {
            InitializeComponent();
        }

        public void LoadInfoByApplicationID(int ApplicationID)
        {
            clsLicenses license = clsLicenses.GetLicensesInfoByAppID(ApplicationID);
            clsPerson Person = clsPerson.FindPersonByID(license.DriverInfo.PersonID);

            lbLicenseClass.Text = clsLicenseClasses.Find(license.LicenseClassID).ClassName;
            lbName.Text = Person.FullName;
            lbLicenseID.Text = license.LicenseID.ToString();
            lbNational_No.Text = lbName.Text = Person.NationalNo;
            
            if (Person.Gendor == 0)
                lbGender.Text = "Female";
            else
                lbGender.Text = "Male";

            lbIssueDate.Text = license.IssueDate.ToString();
            lbIssueReason.Text = license.IssueReason;
            lbExpirationDate.Text = license.ExpirationDate.ToString();  
            lbNotes.Text = license.Notes;
            lbBirthDate.Text =Person.DateOfBirth.ToString();
            lbDriverID.Text = license.DriverID.ToString();
            
            if(license.IsActive)
                lbIsActive.Text = "Yes";
            else
                lbIsActive.Text = "No";

            if (license.isDetained())
                lbIsDetained.Text = "Yes";
            else
                lbIsDetained.Text = "No";

            pbPersonImage.ImageLocation = Person.ImagePath;

        }

        public void LoadInfoByLicenseID(int LicenseID)
        {
            clsLicenses license = clsLicenses.GetLicenseInfo(LicenseID);
            clsPerson Person = clsPerson.FindPersonByID(license.DriverInfo.PersonID);

            lbLicenseClass.Text = clsLicenseClasses.Find(license.LicenseClassID).ClassName;
            lbName.Text = Person.FullName;
            lbLicenseID.Text = license.LicenseID.ToString();
            lbNational_No.Text = lbName.Text = Person.NationalNo;

            if (Person.Gendor == 0)
                lbGender.Text = "Female";
            else
                lbGender.Text = "Male";

            lbIssueDate.Text = license.IssueDate.ToString();
            lbIssueReason.Text = license.IssueReason;
            lbExpirationDate.Text = license.ExpirationDate.ToString();
            lbNotes.Text = license.Notes;
            lbBirthDate.Text = Person.DateOfBirth.ToString();
            lbDriverID.Text = license.DriverID.ToString();

            if (license.IsActive)
                lbIsActive.Text = "Yes";
            else
                lbIsActive.Text = "No";

            if (license.isDetained())
                lbIsDetained.Text = "Yes";
            else
                lbIsDetained.Text = "No";

            pbPersonImage.ImageLocation = Person.ImagePath;

        }


    }
}
