using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_v1
{
    class ADSR
    {
        //attack, decay, sustainTime, and release are defined in seconds
        //sustainLevel is a value between 0 and 1;
        //the time values must observe the constraint:
        //0<attack<decay<sustainTime<release 

        private double envValue;

        public ADSR()
        {
            envValue = 0;
        }

        public double envGenNew(int envIndex, double attack, double decay, double sustainTime, double sustainLevel,
             double SR)
        {
            double zeta = Math.Pow(10, -2/(SR*(decay - attack)));
           // double zetaR = Math.Pow(10, -2/(SR*(release - sustainTime)));

            int attackSamples = (int) (SR*attack);
            int decaySamples = (int) (SR*decay);
            int sustainSamples = (int) (SR*sustainTime);
           // int totalTime = (int) (SR*release);

            double envValueCurr;
            double output;

            envValueCurr = getEnvValue();

            if ((envIndex < attackSamples))
            {
                envValueCurr = envValueCurr + 1.0/(double) attackSamples;
            }
            else if ((envIndex >= attackSamples) && (envIndex < decaySamples))
            {
                envValueCurr = zeta*envValueCurr + (1 - zeta)*sustainLevel;
            }
            else if ((envIndex >= decaySamples) && (envIndex < sustainSamples))
            {
                envValueCurr = sustainLevel;
            }
            /*else if ((envIndex >= sustainSamples) && (envIndex < totalTime))
            {
                envValueCurr = zetaR*envValueCurr;
            }*/
            output = envValueCurr;
            setEnvValue(output);

            return output;
        }

        public void setEnvValue(double envValueCurr)
        {
            envValue = envValueCurr;
        }

        public double getEnvValue()
        {
            double Envelope;
            Envelope = envValue;
            return Envelope;
        }
    }
}
