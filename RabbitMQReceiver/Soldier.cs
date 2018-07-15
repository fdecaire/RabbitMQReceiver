using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQReceiver
{
	public class Soldier
	{
		private readonly int _blockSize = 100;
		public Guid Id { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public int DatabaseNumber { get; set; }

		public Soldier()
		{

		}
		public Soldier(bool createNew)
		{
			if (createNew)
			{
				Id = Guid.NewGuid();
				DatabaseNumber = 0;
			}
		}

		public Soldier(Guid guid, int databaseNumber)
		{
			Id = guid;
			DatabaseNumber = databaseNumber;
		}

		public string BlockAddress
		{
			get
			{
				var x = (X / _blockSize + 1) * _blockSize;
				var y = (Y / _blockSize + 1) * _blockSize;
				return $"SX{x}Y{y}";
			}
		}
	}
}
