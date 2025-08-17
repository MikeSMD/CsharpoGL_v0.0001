using System;

namespace Star
{
	static class DataMiner
	{

		public static ConsoleColor convertStringCc( string inp )
		{
			switch ( inp )
			{
				case "DarkYellow":
					return ConsoleColor.DarkYellow;
					break;
				case "Green":
					return ConsoleColor.Green;
					break;
				default:
					return ConsoleColor.White;
			}
		}
		public static List < Point > getData( string filePath, char sign = '.', ConsoleColor cc = ConsoleColor.White)
		{
		
			if ( !File.Exists( filePath ) )
			{
			 	throw new ArgumentException($"sohbor {filePath} neexistuje");
			}
			var points_list = new List < Point > ();
			var Colors = new Dictionary < ( double, double, double ), (ConsoleColor, char ) >();
			using ( StreamReader reader = new StreamReader(filePath) )
			{
				string line;
				bool data = false;
				while( ( line = reader.ReadLine() ) != null )
				{
             			   string[] parts = line.Split(',');
				   if ( parts [ 0 ] == "-" )
				   {
					   data = true;
					   continue;
				   }

				   if ( ! data )
				   {
					   if ( parts.Length != 5 )
						   continue;

					if( double.TryParse(parts [ 0 ].Trim(), out double x ) && 
					   double.TryParse(parts [ 1 ].Trim(), out double y ) && 
					   double.TryParse(parts [ 2 ].Trim(), out double z ) )
				   	{
						Colors.Add( ( x,y,z ), (convertStringCc( parts [ 3 ] ), parts[ 4 ] [ 0 ] ) );
					}
				   }

				   if ( data )
				   {
					   Point p = null;
					   if ( parts. Length >= 6 )
					   {
					   	if( double.TryParse(parts [ 3 ].Trim(), out double x ) && 
					 	  double.TryParse(parts [ 4 ].Trim(), out double y ) && 
					   	  double.TryParse(parts [ 5 ].Trim(), out double z ) )
						{
							if ( Colors.TryGetValue((x,y,z), out ( ConsoleColor p, char r) qwe ))
							{
								p = new Point ( 3, qwe.r, qwe.p);
							}
						}
					   }

					   if ( p == null )
					   {
						   p = new Point( 3, sign, cc);
					   }
					   if ( p.sign == Specials.randomness )
				   {
					   p.sign = (char)Specials.random.Next(32, 125);
				   }
				   double[] k = new double[ 3 ];

				   int k_index = 0;
				   foreach ( var part in parts )
				   {
					   if ( k_index >= 3 )
						   break;
					   if (double.TryParse(part.Trim(), out double number))
					   {
						   k [ k_index++ ] = number;
					   }
					   else
					   {
						   throw new ArgumentException ("chyba v souboru 1");
					   }
				   }
				   if ( k_index != 3 )
				   {
					   throw new ArgumentException ("chyba v souboru 2");
				   }
				   p.SetCoordinates ( k );
				   points_list.Add( p );
					}
				   }	
				return points_list;
			}
		}

	
		}
}
