using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MohDemo.Utility;

namespace MohDemo.Models
{
	public struct WhereClause
	{
		public string WhereStatement { get; set; }
		public object[] Parameters { get; set; }
	}

	public class WhereStatement<T>
	{
		/// <summary>
		/// This method create dynamically where clause statement for sql server
		/// </summary>
		/// <param name="searchCollections"> Search Dictionary Parameters </param>
		/// <returns> A structure as where statement </returns>
		public static WhereClause CreateWhereClause(Dictionary<string, string> searchCollections)
		{
			string whereClause = string.Empty;
			int count = 0;
			List<KeyValuePair<string, Type>> param = new List<KeyValuePair<string, Type>>();
			foreach (var item in searchCollections)
			{
				if (!string.IsNullOrEmpty(item.Value))
				{
					if (item.Key.Contains("RequestVerificationToken", StringComparison.OrdinalIgnoreCase) || !string.IsNullOrEmpty(item.Value) && (item.Key.Contains("Id") || item.Key.Contains("ID")) && (string.Compare("0", item.Value) == 0 || Convert.ToInt32(item.Value) < 0))
						continue;

					var propertyInfo = typeof(T).GetProperty(item.Key);
					if (string.IsNullOrEmpty(whereClause))
					{
						if (propertyInfo != null)
						{
							if (Type.GetTypeCode(propertyInfo.PropertyType) == TypeCode.String)
								whereClause += $"{item.Key}.Contains(@{count})";
							else if (Type.GetTypeCode(propertyInfo.PropertyType) == TypeCode.DateTime)
							{
								if (item.Key.ToLower().Contains("start") || item.Key.ToLower().Contains("from"))
									whereClause += $"{item.Key} >= @{count}";
								else if (item.Key.ToLower().Contains("end") || item.Key.ToLower().Contains("to"))
									whereClause += $"{item.Key} <= @{count}";
								else
									whereClause += $"{item.Key} = @{count}";
							}
							else
								whereClause += $"{item.Key} = @{count}";
						}
					}
					else
					{
						if (propertyInfo != null)
						{
							if (Type.GetTypeCode(propertyInfo.PropertyType) == TypeCode.String)
								whereClause += $" AND {item.Key}.Contains(@{count})";
							else if (Type.GetTypeCode(propertyInfo.PropertyType) == TypeCode.DateTime)
							{
								if (item.Key.ToLower().Contains("start") || item.Key.ToLower().Contains("from"))
									whereClause += $" AND {item.Key} >= @{count}";
								else if (item.Key.ToLower().Contains("end") || item.Key.ToLower().Contains("to"))
									whereClause += $" AND {item.Key} <= @{count}";
								else
									whereClause += $" AND {item.Key} = @{count}";
							}
							else
								whereClause += $" AND {item.Key} = @{count}";
						}
					}
					if (propertyInfo != null)
					{
						if (Type.GetTypeCode(propertyInfo.PropertyType) == TypeCode.DateTime)
							param.Add(new KeyValuePair<string, Type>(item.Value.ToDateTime().ToString("yyyy/MM/dd"), propertyInfo.PropertyType));
						else
							param.Add(new KeyValuePair<string, Type>(item.Value, propertyInfo.PropertyType));
					}
					count++;
				}
			}

			if (string.IsNullOrEmpty(whereClause))
				whereClause = "1 = 1";

			object[] parameter = new object[param.Count];
			count = 0;
			foreach (KeyValuePair<string, Type> item in param)
			{
				parameter[count] = SetValueType(item);
				count++;
			}

			return new WhereClause() { WhereStatement = whereClause, Parameters = parameter };
		}

		/// <summary>
		/// Get Type of a parameter which is pass to the method
		/// </summary>
		/// <param name="keyValuePair"></param>
		/// <returns> A type of parameter </returns>
		private static object SetValueType(KeyValuePair<string, Type> keyValuePair)
		{
			switch (Type.GetTypeCode(keyValuePair.Value))
			{
				case TypeCode.Int32:
					return Convert.ToInt32(keyValuePair.Key);
				case TypeCode.Boolean:
					return Convert.ToBoolean(keyValuePair.Key);
				case TypeCode.Byte:
					return Convert.ToByte(keyValuePair.Key);
				case TypeCode.Char:
					return Convert.ToChar(keyValuePair.Key);
				case TypeCode.DateTime:
					return Convert.ToDateTime(keyValuePair.Key);
				case TypeCode.Decimal:
					return Convert.ToDecimal(keyValuePair.Key);
				case TypeCode.Double:
					return Convert.ToDouble(keyValuePair.Key);
				case TypeCode.Int16:
					return Convert.ToInt16(keyValuePair.Key);
				case TypeCode.Int64:
					return Convert.ToInt64(keyValuePair.Key);
				case TypeCode.Object:
					return (object)keyValuePair.Key;
				case TypeCode.Single:
					return Convert.ToSingle(keyValuePair.Key);
				case TypeCode.SByte:
					return Convert.ToSByte(keyValuePair.Key);
				case TypeCode.String:
					return keyValuePair.Key.ToString().Trim();
				case TypeCode.UInt16:
					return Convert.ToUInt16(keyValuePair.Key);
				case TypeCode.UInt32:
					return Convert.ToUInt32(keyValuePair.Key);
				case TypeCode.UInt64:
					return Convert.ToUInt64(keyValuePair.Key);
				default:
					return keyValuePair.Key;
			}
		}
	}
}
