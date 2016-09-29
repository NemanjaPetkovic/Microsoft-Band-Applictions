package com.microsoft.band.sdk.sampleapp.accelerometer;

import com.microsoft.band.BandClient;
import com.microsoft.band.BandClientManager;
import com.microsoft.band.BandException;
import com.microsoft.band.BandInfo;
import com.microsoft.band.ConnectionState;
import com.microsoft.band.sensors.BandAccelerometerEvent;
import com.microsoft.band.sensors.BandAccelerometerEventListener;
import com.microsoft.band.sensors.SampleRate;
import android.media.AudioFormat;
import android.media.AudioManager;
import android.media.AudioTrack;
import android.os.Bundle;
import android.view.View;
import android.app.Activity;
import android.os.AsyncTask;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.RadioButton;
import android.widget.TextView;

public class BandAccelerometerAppActivity extends Activity {

	int amp = 10000;
	double twopi = 8.*Math.atan(1.);
	double fr = 440.f;
	double ph = 0.0 , ph1 = 0.0;
	int Dsamps;

	Thread t;
	int sr = 44100;
	boolean isRunning = true;
	short samples[];
	int buffsize;
	AudioTrack audioTrack;

	private BandClient client = null;
	private Button btnStart;
	private TextView txtStatus;
	double xVal,yVal,zVal;

	private BandAccelerometerEventListener mAccelerometerEventListener = new BandAccelerometerEventListener() {
		@Override
		public void onBandAccelerometerChanged(final BandAccelerometerEvent event) {
			if (event != null) {
				xVal=event.getAccelerationX();
				yVal=event.getAccelerationY();
				zVal=event.getAccelerationZ();
				xVal = Math.round(Math.abs(xVal*1000));
				yVal = Math.round(Math.abs(xVal*10));
				zVal = Math.round(Math.abs(xVal*1000));

				appendToUI(String.format("Frequency = %.0f hz \n Vibrato = %.0f ", xVal, yVal));
			}
		}
	};

	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		txtStatus = (TextView) findViewById(R.id.txtStatus);
		btnStart = (Button) findViewById(R.id.btnStart);

		btnStart.setOnClickListener(new OnClickListener() {
			@Override
			public void onClick(View v) {
				txtStatus.setText("");
				new AccelerometerSubscriptionTask().execute();
			}
		});

		t = new Thread() {
			public void run() {
				// set process priority
				setPriority(Thread.MAX_PRIORITY);
				buffsize = AudioTrack.getMinBufferSize(sr,
						AudioFormat.CHANNEL_OUT_MONO, AudioFormat.ENCODING_PCM_16BIT);
				audioTrack = new AudioTrack(AudioManager.STREAM_MUSIC,
						sr, AudioFormat.CHANNEL_OUT_MONO,
						AudioFormat.ENCODING_PCM_16BIT, buffsize,
						AudioTrack.MODE_STREAM);

				Dsamps=buffsize;
				audioTrack.play();
				samples = new short[buffsize];
				RadioButton radioButton1, radioButton2;
				radioButton1 = (RadioButton) findViewById(R.id.radioButton1);
				radioButton2 = (RadioButton) findViewById(R.id.radioButton2);

				while(isRunning)
				{
					if(radioButton1.isChecked())
					{
						sine(samples, buffsize);
						audioTrack.write(samples, 0, buffsize);
					}

					else if(radioButton2.isChecked())
					{
						sineRmod(samples, buffsize);
						audioTrack.write(samples, 0, buffsize);
					}

				}
			}
		};
		t.start();
	}

	public short[] sine(short[] samples, int buffsize){
		fr = xVal;
		for (int i = 0; i < buffsize; i++){
			samples[i] = (short) (amp*0.5 * Math.cos(ph));
			ph += twopi * fr / sr;
		}
		return samples;
	}

	public short[] sineRmod(short[] samples, int buffsize) {
		fr = xVal; //will go from fr to fr + fr
		double fr2=yVal;

		for (int i = 0; i < buffsize; i++) {
			samples[i] = (short) (amp * 0.5 * ((Math.cos(ph) * Math.sin(ph1))));
			ph1 += twopi * fr2/ sr;
			ph += twopi * fr / sr;
		}
		return samples;
	}


	private class AccelerometerSubscriptionTask extends AsyncTask<Void, Void, Void> {
		@Override
		protected Void doInBackground(Void... params) {
			try {
				if (getConnectedBandClient()) {
					appendToUI("Band is connected.\n");
					client.getSensorManager().registerAccelerometerEventListener(mAccelerometerEventListener, SampleRate.MS128);
				} else {
					appendToUI("Band isn't connected. Please make sure bluetooth is on and the band is in range.\n");
				}
			} catch (BandException e) {
				String exceptionMessage;
				switch (e.getErrorType()) {
					case UNSUPPORTED_SDK_VERSION_ERROR:
						exceptionMessage = "Microsoft Health BandService doesn't support your SDK Version. Please update to latest SDK.\n";
						break;
					case SERVICE_ERROR:
						exceptionMessage = "Microsoft Health BandService is not available. Please make sure Microsoft Health is installed and that you have the correct permissions.\n";
						break;
					default:
						exceptionMessage = "Unknown error occured: " + e.getMessage() + "\n";
						break;
				}
				appendToUI(exceptionMessage);

			} catch (Exception e) {
				appendToUI(e.getMessage());
			}
			return null;
		}
	}

	private void appendToUI(final String string) {
		this.runOnUiThread(new Runnable() {
			@Override
			public void run() {
				txtStatus.setText(string);
			}
		});
	}

	private boolean getConnectedBandClient() throws InterruptedException, BandException {
		if (client == null) {
			BandInfo[] devices = BandClientManager.getInstance().getPairedBands();
			if (devices.length == 0) {
				appendToUI("Band isn't paired with your phone.\n");
				return false;
			}
			client = BandClientManager.getInstance().create(getBaseContext(), devices[0]);
		} else if (ConnectionState.CONNECTED == client.getConnectionState()) {
			return true;
		}

		appendToUI("Band is connecting...\n");
		return ConnectionState.CONNECTED == client.connect().await();
	}
}

