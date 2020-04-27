using System.Numerics;


namespace Lab1Model
{
	namespace PassiveComponents
	{
		/// <summary>
		/// ����� ���������
		/// </summary>
		public class Resistor : ComponentBase
		{
			/// <summary>
			/// ��������� ��������� ������ <see cref="Resistor"/>
			/// </summary>
			public Resistor() : base(0) { }

			/// <summary>
			/// ��������� ��������� ������ <see cref="Resistor"/>
			/// </summary>
			/// <param name="value">�������� ������������� � ����</param>
			public Resistor(double value) : base(value) { }

			protected override Complex CalcImpedance(double freq)
			{
				return new Complex(Value, 0);
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
