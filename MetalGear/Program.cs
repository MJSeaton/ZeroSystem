/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 8/1/2011
 * Time: 5:11 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace MetalGear
{
	class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Initiating Metal Gear Startup Sequence:\n Please login to continue: \n" +
			                  "...\n...\n...\n FoxHound ID: The Asterite \n Login Complete!\n Incoming Connection from MidiJane... \n");
			Midi.MidiJane Jane=new Midi.MidiJane();
			SBC.SteelBattalionController Controller=new SBC.SteelBattalionController();
			ModularMetalGear Overlay=new ModularMetalGear(Controller,Jane);
			
			// TODO: Implement Functionality Here
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}