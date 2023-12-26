using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace SBOHelper.Utils
{

	internal enum GetDefaults
	{
		Color = 1,
		SelesEmployee = 2,
		Warehouse = 3,
		CashOnHand = 4,
		ChecksRcvd = 5,
		DftCustAR = 6,
		TaxCode = 7
	}


	public enum UserDetails
	{

		UserCode = 1,
		UserName = 2,
		EMail = 3,
		Mobile = 4,
		SuperUser = 5,
		Fax = 6,
		Defaults = 7,
		Branch = 8,
		Department = 9,
		Locked = 10,
		POSUser = 11,
		AuthType = 12

	}

	public enum SeriesReturnType
	{

		Series = 1,
		Name = 2,
		InitialNumber = 3

	}

	public enum DataSourceType
	{

		DBDataSource = 1,
		UserDataSource = 2

	}

	public enum DateDiffTypes
	{

		Months = 1,
		Days = 2,
		Hours = 3,
		Minutes = 4,
		Seconds = 5

	}
	public enum WarehouseDetails
	{
		Location = 1,
		Name = 2,
        State
	}

	public enum DocType
	{
		AR = 1,
		AP = 2
	}

    public enum DocumentType
    {
        Item = 1,
        Service = 2
    }
    public enum MailUserType
    {
        ContactPerson = 1,
        RandomUser = 2,
        InternalUser = 3
    }

}

