using System;
using System.Linq;

namespace HouseOfSynergy.PowerTools.Library.Media.Audio
{
	public enum AudioSampleRate:
		int
	{
		AudioSampleRate_008000 = 008000, // Telephone and encrypted walkie-talkie, wireless intercom[10][11] and wireless microphone[12] transmission; adequate for human speech but without sibilance; ess sounds like eff (/s/, /f/).
		AudioSampleRate_011025 = 011025, // One quarter the sampling rate of audio CDs; used for lower-quality PCM, MPEG audio and for audio analysis of subwoofer bandpasses.
		AudioSampleRate_016000 = 016000, // Wideband frequency extension over standard telephone narrowband 8,000 Hz. Used in most modern VoIP and VVoIP communication products.
		AudioSampleRate_022050 = 022050, // One half the sampling rate of audio CDs; used for lower-quality PCM and MPEG audio and for audio analysis of low frequency energy. Suitable for digitizing early 20th century audio formats such as 78s.
		AudioSampleRate_032000 = 032000, // MiniDV digital video camcorder, video tapes with extra channels of audio (e.g. DVCAM with 4 Channels of audio), DAT (LP mode), Germany's Digitales Satellitenradio, NICAM digital audio, used alongside analogue television sound in some countries. High-quality digital wireless microphones.[15] Suitable for digitizing FM radio.
		AudioSampleRate_044056 = 044056, // Used by digital audio locked to NTSC color video signals (245 lines by 3 samples by 59.94 fields per second = 29.97 frames per second).
		AudioSampleRate_044100 = 044100, // Audio CD, also most commonly used with MPEG-1 audio (VCD, SVCD, MP3). Originally chosen by Sony because it could be recorded on modified video equipment running at either 25 frames per second (PAL) or 30 frame/s (using an NTSC monochrome video recorder) and cover the 20 kHz bandwidth thought necessary to match professional analog recording equipment of the time. A PCM adaptor would fit digital audio samples into the analog video channel of, for example, PAL video tapes using 588 lines by 3 samples by 25 frames per second.
		AudioSampleRate_047250 = 047250, // World's first commercial PCM sound recorder by Nippon Columbia (Denon)
		AudioSampleRate_048000 = 048000, // The standard audio sampling rate used by professional digital video equipment such as tape recorders, video servers, vision mixers and so on. This rate was chosen because it could deliver a 22 kHz frequency response and work with 29.97 frames per second NTSC video - as well as 25 frame/s, 30 frame/s and 24 frame/s systems. With 29.97 frame/s systems it is necessary to handle 1601.6 audio samples per frame delivering an integer number of audio samples only every fifth video frame.[9]  Also used for sound with consumer video formats like DV, digital TV, DVD, and films. The professional Serial Digital Interface (SDI) and High-definition Serial Digital Interface (HD-SDI) used to connect broadcast television equipment together uses this audio sampling frequency. Most professional audio gear uses 48 kHz sampling, including mixing consoles, and digital recording devices.
		AudioSampleRate_050000 = 050000, // First commercial digital audio recorders from the late 70s from 3M and Soundstream.
		AudioSampleRate_050400 = 050400, // Sampling rate used by the Mitsubishi X-80 digital audio recorder.
		AudioSampleRate_088200 = 088200, // Sampling rate used by some professional recording equipment when the destination is CD (multiples of 44,100 Hz). Some pro audio gear uses (or is able to select) 88.2 kHz sampling, including mixers, EQs, compressors, reverb, crossovers and recording devices.
		AudioSampleRate_096000 = 096000, // DVD-Audio, some LPCM DVD tracks, BD-ROM (Blu-ray Disc) audio tracks, HD DVD (High-Definition DVD) audio tracks. Some professional recording and production equipment is able to select 96 kHz sampling. This sampling frequency is twice the 48 kHz standard commonly used with audio on professional equipment.
		AudioSampleRate_176400 = 176400, // Sampling rate used by HDCD recorders and other professional applications for CD production.
		AudioSampleRate_192000 = 192000, // DVD-Audio, some LPCM DVD tracks, BD-ROM (Blu-ray Disc) audio tracks, and HD DVD (High-Definition DVD) audio tracks, High-Definition audio recording devices and audio editing software. This sampling frequency is four times the 48 kHz standard commonly used with audio on professional video equipment.
	}
}