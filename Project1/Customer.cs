using System;

public class Customer
{
    private String companyNameValue;
    private String contactNameValue;

    public Customer()
    {
        // Leave fields empty.
    }

    public Customer(String companyName, String contactName)
    {
        companyNameValue = companyName;
        contactNameValue = contactName;
    }

    public String CompanyName
    {
        get => companyNameValue;
        set => companyNameValue = value;
    }

    public String ContactName
    {
        get => contactNameValue;
        set => contactNameValue = value;
    }
}