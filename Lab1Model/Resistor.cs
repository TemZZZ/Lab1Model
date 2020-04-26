namespace Lab1Model
{
	namespace PassiveComponents
	{
		/// <summary>
		/// ����� ���������
		/// </summary>
		public class Resistor : ComponentBase
		{
			public Resistor() { Value = 0; }
			public Resistor(double value) { Value = value; }

			protected override double CalcImpedance(double freq)
			{
				return Value;
			}

			/// <summary>
			/// ���������� ������������� ���������
			/// </summary>
			/// <returns>������������� � ����</returns>
			public double GetImpedance()
			{
				return Value;
			}

			/// <summary>
			/// ���������� ��������� ������������� �������
			/// </summary>
			/// <returns>������ ���� "Resistance = {R} ohms"</returns>
			public override string ToString()
			{
				return $"Resistance = {Value} ohms";
			}
		}
	}
}
