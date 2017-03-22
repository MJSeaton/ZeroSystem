/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 8/3/2011
 * Time: 10:38 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Midi
{
	/// <summary>
	/// Description of Class3.
	/// </summary>
	public class MidiJane
	{
		public bool throttleflickerstopper;
		public bool MAINBLOCK;
        public bool DetachRight;
		public OutputDevice GiantsDrink;
		
		
		public MidiJane(){
            this.DetachRight = false;
		this.throttleflickerstopper=false;
		this.MAINBLOCK=false;
		//Console.WriteLine("*MidiJane: Ah, The Asterite! Good to see you again! \n*MidiJane:I'll get the MMG systems up and running. \n *MidiJane:Relax and remember: The Enemy's Gate is Down. \n*MidiJane:FINDING THE PERFECT INTERFACING TOOL...\n*MidiJane:...\n*MidiJane:..\n*MidiJane:.\n*MidiJane:Let's see...\n");
			foreach (OutputDevice Out in OutputDevice.InstalledDevices){
				Console.WriteLine(Out.Name+"\n");
				if( Out.Name=="loopMIDI Port"){
					Console.WriteLine("*MidiJane:That's the one! loopMIDI Port Found!\n*MidiJane:Now Linking:\n");
					Out.Open();
					GiantsDrink=Out;
					if (GiantsDrink==Out){
						Console.WriteLine("*MidiJane:Link estanblished. I've got to act as a bridge now. \n*MidiJane:See you on the other side! \n{CLASSIFICATION:AI.ACTION}*MidiJane has entered bridge mode");
					break;
					}
					else{
						Console.WriteLine("*MidiJane:Linkage Failure!");
					}
					
					}
				else{
					Console.WriteLine("*MidiJane:This isn't it.\n*MidiJane:Is LoopMIDI running?");
				
				}
				}
			}
	
		//This is the Joysender code. Should be disabled to calibrate button presses etc

			
		public void RIGHTJOYSENDER(int joyX, int joyY){
		 	GiantsDrink.SendControlChange(Midi.Channel.Channel1, Control.SustainPedal,joyX);
			
			GiantsDrink.SendControlChange(Midi.Channel.Channel1, Control.ReverbLevel,joyY);
			
		}
		public void LEFTLEFTSENDER(int pos){
		GiantsDrink.SendControlChange(Midi.Channel.Channel1, Control.TremoloLevel,pos);
		}
		public void LEFTRIGHTSENDER(int pos){
		GiantsDrink.SendControlChange(Midi.Channel.Channel1, Control.Pan, pos);
		}
		
		public void SIGHTCHANGESENDER(int SIGHTX, int SIGHTY){
			GiantsDrink.SendControlChange(Midi.Channel.Channel1, Control.ModulationWheel,SIGHTX);
			GiantsDrink.SendControlChange(Midi.Channel.Channel1, Control.PhaserLevel,SIGHTY);	
		}
		public void ThrottleHandling(int throttleinput){
			if(this.throttleflickerstopper==true){
				Console.WriteLine("flickerstop is on, turning off");
				throttleflickerstopper=false;
			}
			else{
			
		//	Console.WriteLine(throttleinput);
			}
		}
		public void THIRTEENTHWARRIOR(String BUTTONINPUT){
			// Use a for loop to examine each one of the states returned in the state change array
			String name=BUTTONINPUT;
					// Write out the state of the button if it was changed
					Console.WriteLine("Said before throttlehandling: "+ name);
					
		
					if(BUTTONINPUT=="RightJoyLockOn"){
						}
					
					else if(BUTTONINPUT=="CockpitHatch"){
						this.GiantsDrink.SendNoteOn(Channel.Channel1,Pitch.DSharp0,127);
					}
					else if(BUTTONINPUT=="Ignition"){
						this.GiantsDrink.SendNoteOn(Channel.Channel1,Pitch.E0, 127);
					}
					else if(BUTTONINPUT=="MainMonZoomIn"){
						this.GiantsDrink.SendNoteOn(Channel.Channel1,Pitch.F0, 127);
					}
					else if(BUTTONINPUT=="MainMonZoomOut"){
						this.GiantsDrink.SendNoteOn(Channel.Channel1,Pitch.FSharp0,127);
					}
					else if(BUTTONINPUT=="MultiMonOpenClose"){
						this.GiantsDrink.SendNoteOn(Channel.Channel1,Pitch.G0, 127);
					}
					else if(BUTTONINPUT=="MultiMonMapZoomInOut"){
						this.GiantsDrink.SendNoteOn(Channel.Channel1, Pitch.GSharp0, 127);
					}
					else if(BUTTONINPUT=="WeaponConMain"){
						this.GiantsDrink.SendNoteOn(Channel.Channel1, Pitch.A0, 127);
					}
					else if(BUTTONINPUT=="WeaponConMagazine"){
						this.GiantsDrink.SendNoteOn(Channel.Channel1, Pitch.B0, 127);
					}
					else if(BUTTONINPUT=="FunctionF3"){
						this.GiantsDrink.SendNoteOn(Channel.Channel1,Pitch.C1,127);
					}
					else if(BUTTONINPUT=="FunctionNightScope"){
						this.GiantsDrink.SendNoteOn(Channel.Channel1,Pitch.CSharp1, 127);
					}
					else if(BUTTONINPUT=="FunctionLineColorChange"){
				//currently used for internal statekeeping rather than midi triggering		
                //this.GiantsDrink.SendNoteOn(Channel.Channel1, Pitch.D1, 127);
                if (this.DetachRight == true)
                {
                    this.DetachRight = false;
                }        
                else if (this.DetachRight == false)
                {
                    Console.WriteLine("Right Block Detached Bool Set to true\n");
                    this.DetachRight = true;
                }
            }
					else if(BUTTONINPUT=="LeftJoySightChange"){
						this.GiantsDrink.SendNoteOn(Channel.Channel1, Pitch.DSharp1, 127);
					}
					else if(BUTTONINPUT=="TunerDialStateChange"){
						
					}
					
			}
			
		}
}

		
		
	

