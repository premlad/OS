namespace OS.Data_Entity
{
	public class T_INS_PER
	{
		public string ENTEREDBY { get; set; }
		public string ID { get; set; }
		public string CONNECT_PERSON_ID { get; set; }
		public string NAME_OF_EMP { get; set; }
		public string CURRENT_DESIGNATION { get; set; }
		public string ADDRESS { get; set; }
		public string PAN_NO { get; set; }
		public string DEMAT_AC_NO { get; set; }
		public string MOBILE_NO { get; set; }
		public string GRADUATION_INSTITUTIONS { get; set; }
		public string PAST_EMPLOYEES { get; set; }
		public T_INS_PER_IMMEDAITE_RELATIVES[] RELATIVEARRAY { get; set; }
		public T_INS_PER_MATERIAL_FINANCIAL[] FINANCIALARRAY { get; set; }
	}

	public class T_INS_PER_IMMEDAITE_RELATIVES
	{
		public string NAME { get; set; }
		public string ADDRESS { get; set; }
		public string RELATIONSHIP { get; set; }
		public string MOBILE_NO { get; set; }
		public string PAN_NO { get; set; }
		public string DEMAT_AC_NO { get; set; }
	}

	public class T_INS_PER_MATERIAL_FINANCIAL
	{
		public string NAME { get; set; }
		public string ADDRESS { get; set; }
		public string RELATIONSHIP { get; set; }
		public string MOBILE_NO { get; set; }
		public string PAN_NO { get; set; }
		public string DEMAT_AC_NO { get; set; }
	}
}
