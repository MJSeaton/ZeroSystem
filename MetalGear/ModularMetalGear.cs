/*
 * Created by SharpDevelop.
 * User: Matthew
 * Date: 8/1/2011
 * Time: 5:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace MetalGear
{
	/// <summary>0.
	/// This is the Modular Metal Gear Class, which takes in a controller instance for local reference and event Handling. It also takes in a MIDIJANE for MIDI Handling
	/// </summary>
	unsafe public class ModularMetalGear
	{
        public bool useless;
		public SBC.SteelBattalionController Controller;
		public Midi.MidiJane JANEBRIDGE;
		public int gear;
		public int lastgearpolled;
		public int PREVTUNER;
		public bool EQ2ACTIVE;
		public bool CHOPMODE;
		public bool BLUEDUALCHOP;
		public bool BLUEEQ;
		public bool BLOCKTRIPS;
		public bool TEMPOINCREASE;
		public bool TEMPODECREASE;
		public bool[] COMMSTATES;
		public bool CUEB;
		public bool CUEA;
		public bool EQB;
		public bool EQZERO1;
		public bool EQZERO2;
		public bool[] EQONE;
		public bool[] EQTWO;
		public bool EQ1ACTIVE;
		public bool LEFTMUTE;
		public bool RIGHTMUTE;
		public bool BUTTONLOCK;
		public bool LAUNCH;
		public bool LOCKEDON;
		public bool CATCHING;
		public ModularMetalGear(SBC.SteelBattalionController Contr, Midi.MidiJane Jane)
		{
			CUEA=false;
			EQ2ACTIVE=false;
			CATCHING=false;
			COMMSTATES=new bool[5]{false, false, false, false, false};
			EQONE=new bool[3]{false,false,false};
			EQTWO=new bool[3]{false,false,false};
			LOCKEDON=false;
			LAUNCH=false;
			gear=Contr.GearLever;
			lastgearpolled=gear;
			LEFTMUTE=false;
			RIGHTMUTE=false;
			EQ1ACTIVE=false;
			CUEB=false;
			EQB=false;
			EQZERO1=false;
			EQZERO2=false;
			TEMPODECREASE=false;
			TEMPOINCREASE=false;
			BLUEDUALCHOP=false;
			BLUEEQ=false;
			BLOCKTRIPS=false;
			CHOPMODE= false;
			Controller = Contr;
			JANEBRIDGE=Jane;
			Controller.Init(50);
			handler(Controller);
		}
		public void handler(SBC.SteelBattalionController Contr){
			// Add the event handler to monitor button state changed events
			Controller.ButtonStateChanged += new SBC.SteelBattalionController.ButtonStateChangedDelegate(controller_ButtonStateChanged);
			
			// Run in an infinite loop
			while(1 == 1) { System.Threading.Thread.Sleep(10); }
		}
		

		void controller_ButtonStateChanged(SBC.ButtonState[] stateChangedArray) {
	
	RightJoyHandling();
		SightChangeHandling();
		RoatationHandling(Controller.RotationLever);
		///
		///Lockon Handling
		///
	
		if(stateChangedArray[(int) SBC.ButtonEnum.RightJoyLockOn].changed){
			if(stateChangedArray[(int) SBC.ButtonEnum.RightJoyLockOn].currentState){
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1,Midi.Pitch.ASharp4, 127);
			}
		}
		
		///
		////COMMBUTTON Handling
		///
		if (stateChangedArray[(int) SBC.ButtonEnum.Comm1].changed &&stateChangedArray[(int) SBC.ButtonEnum.Comm1].currentState){
				if(this.COMMSTATES[0]==false){
					this.COMMSTATES[0]=true;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.Comm1, 15);
				}
				else{
					this.COMMSTATES[0]=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.Comm1, 3);
				}
					
			}
			else if (stateChangedArray[(int) SBC.ButtonEnum.Comm2].changed&&stateChangedArray[(int) SBC.ButtonEnum.Comm2].currentState){
				if(this.COMMSTATES[1]==false){
					this.COMMSTATES[1]=true;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.Comm2, 15);
				}
				else{
					this.COMMSTATES[1]=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.Comm2, 3);
				}
			}
		
		if (stateChangedArray[(int) SBC.ButtonEnum.Comm3].changed && stateChangedArray[(int) SBC.ButtonEnum.Comm3].currentState){
				if(this.COMMSTATES[2]!=true){
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Comm3, 15);
				
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Comm4, 3);	
				this.COMMSTATES[2]=true;
				this.COMMSTATES[3]=false;
				this.JANEBRIDGE.GiantsDrink.SendControlChange(Midi.Channel.Channel3,Midi.Control.Volume, 0);
				}
			else{
				this.COMMSTATES[2]=false;
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Comm3, 3);
				
				this.JANEBRIDGE.GiantsDrink.SendControlChange(Midi.Channel.Channel3,Midi.Control.Volume, 55);
				}
		
			}
		
		else if (stateChangedArray[(int) SBC.ButtonEnum.Comm4].changed  && stateChangedArray[(int) SBC.ButtonEnum.Comm4].currentState){
				if(this.COMMSTATES[3]!=true){
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Comm4, 15);
				
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Comm3, 3);	
				this.COMMSTATES[3]=true;
				this.COMMSTATES[2]=false;
			
				this.JANEBRIDGE.GiantsDrink.SendControlChange(Midi.Channel.Channel3,Midi.Control.Volume, 127);
			}
			else{
				this.COMMSTATES[3]=false;
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Comm4, 3);
				
				this.JANEBRIDGE.GiantsDrink.SendControlChange(Midi.Channel.Channel3,Midi.Control.Volume, 55);
				
			}
			}
		
		/// <summary>
		/// actually for loopercue
		/// </summary>
		/// <param name="input"></param>
			if (stateChangedArray[(int) SBC.ButtonEnum.Comm5].changed){
			if (stateChangedArray[(int) SBC.ButtonEnum.Comm5].currentState){
				SBC.ControllerLEDEnum LightId = (SBC.ControllerLEDEnum.Comm5);
				if(this.CHOPMODE==true){
					this.Controller.SetLEDState(LightId, 3);
					this.CHOPMODE=false;
				}
				else{
					this.CHOPMODE=true;
					this.Controller.SetLEDState(LightId, 15);
		}
				/// <summary>
				/// BLUENINEBLOCK
				/// </summary>
				/// Sets Destination for tuner dial changes, 3 state switch
				/// <param name="rawData"></param>
				///
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1,Midi.Pitch.B5,127);
			}
		}
            
			if(stateChangedArray[(int) SBC.ButtonEnum.FunctionF1].changed){
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.F1, 15);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.TankDetach, 5);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.ForecastShootingSystem, 5);
				this.EQONE[0]=true;

                //new code below handling note output for weapon seleect
                this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel4, Midi.Pitch.G0, 127);

                if (this.EQONE[1] == true)
                {
                    //giantsdrink output statement here
                    this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel4, Midi.Pitch.GSharp0, 127);

                }
                else if (this.EQONE[2] == true)
                {
                    this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel4, Midi.Pitch.A0, 127);
                }
                this.EQONE[1]=false;
				this.EQONE[2]=false;
			}

			else if(stateChangedArray[(int) SBC.ButtonEnum.FunctionTankDetach].changed){
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.TankDetach, 15);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.F1,5);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.ForecastShootingSystem,5);
                this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel4, Midi.Pitch.GSharp0,127);

                if (this.EQONE[0] == true)
                {
                    //giantsdrink output statement here
                    this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel4, Midi.Pitch.G0, 127);

                }
                else if (this.EQONE[2] == true)
                {
                    this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel4, Midi.Pitch.A0, 127);
                }
                this.EQONE[0]=false;
				this.EQONE[1]=true;
				this.EQONE[2]=false;
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.FunctionFSS].changed){
   
                this.Controller.SetLEDState(SBC.ControllerLEDEnum.ForecastShootingSystem,15);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.F1,5);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.TankDetach,5);
                this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel4, Midi.Pitch.A0, 127);

                if (this.EQONE[0] == true)
                {
                    //giantsdrink output statement here
                    this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel4, Midi.Pitch.G0, 127);

                }
                else if (this.EQONE[1] == true)
                {
                    this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel4, Midi.Pitch.GSharp0, 127);
                }

                this.EQONE[0]=false;
				this.EQONE[1]=false;
				this.EQONE[2]=true;
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.FunctionF2].changed){
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.F2,15);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Override, 5);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Manipulator, 5);
				this.EQTWO[0]=true;
				this.EQTWO[1]=false;
				this.EQTWO[2]=false;
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.FunctionOverride].changed){
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Override,15);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Manipulator,5);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.F2,5);
				this.EQTWO[0]=false;
				this.EQTWO[1]=true;
				this.EQTWO[2]=false;
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.FunctionManipulator].changed){
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Manipulator,15);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.F2,5);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Override,5);
				this.EQTWO[0]=false;
				this.EQTWO[1]=false;
				this.EQTWO[2]=true;
			}
		
			if (stateChangedArray[(int) SBC.ButtonEnum.FunctionF3].changed &&stateChangedArray[(int) SBC.ButtonEnum.FunctionF3].currentState){
				if(this.BLOCKTRIPS!=true){
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.F3,15);
				this.BLOCKTRIPS=true;
				}
				else{
					this.BLOCKTRIPS=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.F3, 2);
				}
			this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel3, Midi.Pitch.GSharp8, 127);
			}
		
			else if(stateChangedArray[(int) SBC.ButtonEnum.FunctionNightScope].changed &&stateChangedArray[(int) SBC.ButtonEnum.FunctionNightScope].currentState){
				if(this.BLUEEQ!=true){
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.NightScope,15);
					this.BLUEEQ=true;
				}
				else{
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.NightScope,2);
					this.BLUEEQ=false;
				}
		
			this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel3,Midi.Pitch.CSharp7, 127);
				
			
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.FunctionLineColorChange].changed && stateChangedArray[(int) SBC.ButtonEnum.FunctionLineColorChange].currentState){
				if(this.BLUEDUALCHOP!=true){
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.LineColorChange,15);
					this.BLUEDUALCHOP=true;
                    this.JANEBRIDGE.DetachRight = true;
                    Console.WriteLine("DetachRight Set to true");
				}
				else{
                    this.JANEBRIDGE.DetachRight = false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.LineColorChange,2);
					this.BLUEDUALCHOP=false;
                    Console.WriteLine("DetachRight Set to False");
				}
			
			this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel3, Midi.Pitch.FSharp8, 127);
			}
			/// <summary>
			/// 
			/// NAV and TEMPO CONTROL LIGHTS (momentary)
			/// </summary>
			/// <param name="rawData"></param>
			/// 
			if(stateChangedArray[(int) SBC.ButtonEnum.Extinguisher].changed){
				if(stateChangedArray[(int) SBC.ButtonEnum.Extinguisher].currentState){
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Extinguisher,15);
				
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel3,Midi.Pitch.B4, 127);
				}
				else{
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.Extinguisher,5);
				}
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.WeaponConSub].changed){
				if(stateChangedArray[(int) SBC.ButtonEnum.WeaponConSub].currentState){
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.SubWeaponControl,15);
				
				
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel3,Midi.Pitch.B5, 127);
			}
				else{
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.SubWeaponControl,5);
				}
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.WeaponConMain].changed && stateChangedArray[(int) SBC.ButtonEnum.WeaponConMain].currentState){
			                          	this.Controller.SetLEDState(SBC.ControllerLEDEnum.MainWeaponControl, 15);
			                          	this.Controller.SetLEDState(SBC.ControllerLEDEnum.MagazineChange, 5);
			                          	this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.A0, 127);
			                          }
			else if(stateChangedArray[(int) SBC.ButtonEnum.WeaponConMagazine].changed && stateChangedArray[(int) SBC.ButtonEnum.WeaponConMagazine].currentState){
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.MagazineChange,15);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.MainWeaponControl,5);
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.B0, 127);
			}
			if (stateChangedArray[(int) SBC.ButtonEnum.Washing].changed){
				if (this.TEMPODECREASE!=true){
					this.TEMPODECREASE=true;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.Washing,15);
					this.JANEBRIDGE.GiantsDrink.SendControlChange(Midi.Channel.Channel3,Midi.Control.Pan, 127);
				}
			
				else{
					this.TEMPODECREASE=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.Washing,3);
				}
			}
		
                      
			if(stateChangedArray[(int) SBC.ButtonEnum.Chaff].currentState){
				if (this.TEMPOINCREASE!=true){
					this.TEMPOINCREASE=true;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.Chaff,15);
					this.JANEBRIDGE.GiantsDrink.SendControlChange(Midi.Channel.Channel3,Midi.Control.Pan, 1);
				}
			}
			
			else{
					this.TEMPOINCREASE=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.Chaff,3);
				}
			
		
			 if(stateChangedArray[(int) SBC.ButtonEnum.TunerDialStateChange].changed){
				this.TunerDialHandling(this.Controller.TunerDial);
			}
			/// <summary>
			/// ////
			/// This is the Light control for the Right SIXBLOCK, which is split L/R
			/// </summary>
			/// <param name="rawData"></param>
			///
			if(stateChangedArray[(int) SBC.ButtonEnum.MainMonZoomIn].changed && stateChangedArray[(int) SBC.ButtonEnum.MainMonZoomIn].currentState){
				if (this.LEFTMUTE!=true){
				this.LEFTMUTE=true;
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.MainMonitorZoomIn,15);
				}
				else{
					this.LEFTMUTE=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.MainMonitorZoomIn,3);
					
					
				}
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel3, Midi.Pitch.CSharp0, 127);
				
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.MainMonZoomOut].changed && stateChangedArray[(int) SBC.ButtonEnum.MainMonZoomOut].currentState){
				if( this.RIGHTMUTE!=true){
					this.RIGHTMUTE=true;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.MainMonitorZoomOut,15);
				}
				else{
					this.RIGHTMUTE=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.MainMonitorZoomOut,3);
					
				}
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel3, Midi.Pitch.DSharp2, 127);
			}
			
			else if(stateChangedArray[(int) SBC.ButtonEnum.MultiMonOpenClose].changed&&stateChangedArray[(int) SBC.ButtonEnum.MultiMonOpenClose].currentState){
				if(this.EQ2ACTIVE==true){
					this.EQ2ACTIVE=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.OpenClose, 3);
				}
				else{
					this.EQ2ACTIVE=true;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.OpenClose,15);
				}
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.FSharp6, 127);
				
				
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.MultiMonMapZoomInOut].changed&&stateChangedArray[(int) SBC.ButtonEnum.MultiMonMapZoomInOut].currentState){
				if (this.EQ2ACTIVE==true){
					this.EQ2ACTIVE=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.MapZoomInOut,3);
				}
				else{
					this.EQ2ACTIVE=true;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.MapZoomInOut,15);
				}
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.FSharp5, 127);
			}
			/// <summary>
			/// /Top L is CuE channel, middle is an EQ-ZEROED indicator (and related fuction)
			/// </summary>
			/// <param name="rawData"></param>
			/// 
			else if(stateChangedArray[(int) SBC.ButtonEnum.MultiMonModeSelect].changed&& stateChangedArray[(int) SBC.ButtonEnum.MultiMonModeSelect].currentState){
				if(this.EQZERO1!=true){
					EQZERO1=true;
					EQZERO2=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.ModeSelect,15);
				}
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel3, Midi.Pitch.FSharp3, 127);
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.SubMonitorModeSelect,3);
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.MultiMonSubMonitor].changed&& stateChangedArray[(int) SBC.ButtonEnum.MultiMonSubMonitor].currentState){
				if(this.EQZERO2!=true){
					EQZERO2=true;
					EQZERO1=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.SubMonitorModeSelect,15);
				}

				this.Controller.SetLEDState(SBC.ControllerLEDEnum.ModeSelect,3);			
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel3, Midi.Pitch.GSharp3, 127);
				
		}
		////////
		/// /////////
			if(stateChangedArray[(int) SBC.ButtonEnum.CockpitHatch].changed && stateChangedArray[(int) SBC.ButtonEnum.CockpitHatch].currentState){
				if(this.CUEA==false){
					this.CUEA=true;
				
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.CockpitHatch,15);
				}
				else{
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.CockpitHatch,3);
					this.CUEA=false;
				}
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1,Midi.Pitch.DSharp0,127);
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.Ignition].changed && stateChangedArray[(int) SBC.ButtonEnum.Ignition].currentState){
				if(this.CUEB==false){
				
				this.CUEB=true;
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.Ignition,15);
				}
				else{
					this.CUEB=false;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.Ignition,3);
				}
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1,Midi.Pitch.E0, 127);
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.Eject].changed && stateChangedArray[(int) SBC.ButtonEnum.Eject].currentState){
				this.BUTTONLOCK=true;
				this.Controller.SetLEDState(SBC.ControllerLEDEnum.EmergencyEject,15);
			}
			/// <summary>
			/// startbutton handling
			/// </summary>
			/// <param name="input"></param>
			 if(this.LAUNCH!=true){
				if(stateChangedArray[(int) SBC.ButtonEnum.Start].changed){
					LAUNCH=true;
					this.Controller.SetLEDState(SBC.ControllerLEDEnum.Start,15);
				}
			}
			/// <summary>
			/// Handling of Toggle Switches Follows
			/// FILT CONTROL for REPEAT OXYGENSUPPLY for CHOPPER
			/// 
			/// </summary>
			/// <param name="input"></param>
			if(stateChangedArray[(int) SBC.ButtonEnum.ToggleFilterControl].changed){
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1,Midi.Pitch.E1,127);
				
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.ToggleOxygenSupply].changed){
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.F1,127);
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.ToggleFuelFlowRate].changed){
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.FSharp1,127);
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.ToggleBuffreMaterial].changed){
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.G1, 127);
			}
			else if(stateChangedArray[(int) SBC.ButtonEnum.ToggleVTLocation].changed){
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.GSharp1,127);
			}
			if(stateChangedArray[(int) SBC.ButtonEnum.LeftJoySightChange].changed && stateChangedArray[(int) SBC.ButtonEnum.LeftJoySightChange].currentState){
	
				this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.CSharp8, 127);
			}
			/// <summary>
			/// ///////
			/// main_weapon and most importantly Triggerhandling follows.
			/// COMM1 and COMM2 set full Auto for Chop and Repeat
			/// LOCKEDON Makes the trigger act as a FullAuto loopergrab
			/// ////////
			/// </summary>
			/// <param name="input"></param>
			
			if(stateChangedArray[(int) SBC.ButtonEnum.RightJoyFire].changed){
				if(stateChangedArray[(int) SBC.ButtonEnum.RightJoyMainWeapon].changed){
					this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1,Midi.Pitch.CSharp0, 127);
				}
				else{
					
						
					
					if(this.COMMSTATES[0]==true){
						this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.C0, 127);
						if(this.COMMSTATES[1]==true){
						this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.C3, 127);

						}
						else{
							if(stateChangedArray[(int) SBC.ButtonEnum.RightJoyFire].currentState){
								this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1,Midi.Pitch.C3, 127);
							}
						}
					}
					
					else{
						if(this.COMMSTATES[1]==true){
							this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.C3, 127);
							if(stateChangedArray[(int) SBC.ButtonEnum.RightJoyFire].currentState){
								this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.C0, 127);
						
							}
						}
						else if(stateChangedArray[(int) SBC.ButtonEnum.RightJoyFire].currentState){
								this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.C3, 127);
								this.JANEBRIDGE.GiantsDrink.SendNoteOn(Midi.Channel.Channel1, Midi.Pitch.C0, 127);
							}
						
					
					
						
					}
				}
			
			
	
				

			
			
			
			
			// Use a for loop to examine each one of the states returned in the state change array
		///	foreach(SBC.ButtonState state in stateChangedArray) {
	//			string statestring=state.button.ToString();
				/// <summary>
				/// here we have the ones that send note ons both when pressed and releseased (toggles)
				/// </summary>
				/// <param name="rawData"></param>
				
	//			if (state.changed&&state.currentState) {
					// Write out the state of the button if it was changed
					
	//				this.JANEBRIDGE.THIRTEENTHWARRIOR(statestring);
					
			//	}
		//	}
			
			int gearint= Controller.GearLever;
				JANEBRIDGE.ThrottleHandling(Controller.GearLever);
				if (gearint==this.lastgearpolled){
				
				if(gearint==1){
					Controller.SetLEDState(SBC.ControllerLEDEnum.Gear1,15);
				}
				else if(gearint==2){
					Controller.SetLEDState(SBC.ControllerLEDEnum.Gear2,15);
				}
				else if(gearint==3){
				Controller.SetLEDState(SBC.ControllerLEDEnum.Gear3,15);
				}
				else if(gearint==4){
					Controller.SetLEDState(SBC.ControllerLEDEnum.Gear4,15);
				}
				else if(gearint==5){
					Controller.SetLEDState(SBC.ControllerLEDEnum.Gear5,15);
				}
				else if(gearint==255){
				Controller.SetLEDState(SBC.ControllerLEDEnum.GearN,15);
				}
				else if(gearint==254){
				Controller.SetLEDState(SBC.ControllerLEDEnum.GearR,15);
				}
				}
			this.lastgearpolled=gearint;
				
			}
		
		}
		

		public void RoatationHandling(int input){
			bool leftmove=false;
			if(input>127){
				input-=128;
			leftmove=true;
			}
			if (leftmove==true){
				this.JANEBRIDGE.LEFTLEFTSENDER(input);
			}
			else{
				this.JANEBRIDGE.LEFTRIGHTSENDER(input);
			}
		}
		public void SightChangeHandling(){
			int AIMINGXCC=(this.Controller.SightChangeX/2);
			int AIMINGYCC=(this.Controller.SightChangeY/2);
			this.JANEBRIDGE.SIGHTCHANGESENDER(AIMINGXCC, AIMINGYCC);
		}
		public void RightJoyHandling(){
            if (this.JANEBRIDGE.DetachRight == true)
            {
            }
            else
            {
                int AIMINGXCC = (this.Controller.AimingX / 2);
                int AIMINGYCC = (this.Controller.AimingY / 2);
                this.JANEBRIDGE.RIGHTJOYSENDER(AIMINGXCC, AIMINGYCC);
            }
		}
		static void controller_RawData(byte[] rawData) {
			Console.WriteLine(BitConverter.ToString(rawData));
		}
		public void TunerDialHandling(int DialPos){
			if (this.PREVTUNER==0){
				if(DialPos==15){
					Console.WriteLine("Downwards Rollover!");
					Console.WriteLine("Tick Down");
					this.JANEBRIDGE.GiantsDrink.SendControlChange(Midi.Channel.Channel3, Midi.Control.ChorusLevel, 127);
					
					}
			else if(this.PREVTUNER==15){
				if(DialPos==0){
					Console.WriteLine("Upwards Rolover!");
					Console.WriteLine("Tick Up");
					this.JANEBRIDGE.GiantsDrink.SendControlChange(Midi.Channel.Channel3, Midi.Control.ChorusLevel, 1);
					}
					
				}
			}
			else{
				if(DialPos>this.PREVTUNER){
					Console.WriteLine("Tick Up");
					this.JANEBRIDGE.GiantsDrink.SendControlChange(Midi.Channel.Channel3, Midi.Control.ChorusLevel, 1);
				}
				else{
					Console.WriteLine("Tick Down");
					this.JANEBRIDGE.GiantsDrink.SendControlChange(Midi.Channel.Channel3, Midi.Control.ChorusLevel, 127);
				}
			}
			this.PREVTUNER=DialPos;
		
		}
	}
}