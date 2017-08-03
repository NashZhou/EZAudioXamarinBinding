using System;
using AVFoundation;
using Accelerate;
using AudioToolbox;
using AudioUnit;
using CoreAnimation;
using CoreAudioKit;
using CoreGraphics;
using Foundation;
using GLKit;
using ObjCRuntime;
using UIKit;

namespace EZAudioKit
{
    // @interface EZAudioDevice : NSObject
    [BaseType(typeof(NSObject))]
	interface EZAudioDevice
	{
        // +(EZAudioDevice *)currentInputDevice;
        [Static]
        [Export("currentInputDevice")]
        EZAudioDevice CurrentInputDevice();

        // +(EZAudioDevice *)currentOutputDevice;
        [Static]
        [Export("currentOutputDevice")]
        EZAudioDevice CurrentOutputDevice();

        // +(NSArray *)inputDevices;
        [Static]
        [Export("inputDevices")]
        EZAudioDevice[] InputDevices();

        // +(NSArray *)outputDevices;
        [Static]
        [Export("outputDevices")]
        EZAudioDevice[] OutputDevices();

		// +(void)enumerateInputDevicesUsingBlock:(void (^)(EZAudioDevice *, BOOL *))block;
		[Static]
		[Export("enumerateInputDevicesUsingBlock:")]
        //todo write wrapper: had bool*
        void EnumerateInputDevicesUsingBlock(Action<EZAudioDevice, IntPtr> block);

		// +(void)enumerateOutputDevicesUsingBlock:(void (^)(EZAudioDevice *, BOOL *))block;
		[Static]
		[Export("enumerateOutputDevicesUsingBlock:")]
        //todo write wrapper: had bool*
        void EnumerateOutputDevicesUsingBlock(Action<EZAudioDevice, IntPtr> block);

		// @property (readonly, copy, nonatomic) NSString * name;
		[Export("name")]
		string Name { get; }

		// @property (readonly, nonatomic, strong) AVAudioSessionPortDescription * port;
		[Export("port", ArgumentSemantic.Strong)]
		AVAudioSessionPortDescription Port { get; }

		// @property (readonly, nonatomic, strong) AVAudioSessionDataSourceDescription * dataSource;
		[Export("dataSource", ArgumentSemantic.Strong)]
		AVAudioSessionDataSourceDescription DataSource { get; }
	}

	// @interface EZAudioFloatData : NSObject
	[BaseType(typeof(NSObject))]
	interface EZAudioFloatData
	{
		// +(instancetype)dataWithNumberOfChannels:(int)numberOfChannels buffers:(float **)buffers bufferSize:(UInt32)bufferSize;
		[Static]
		[Export("dataWithNumberOfChannels:buffers:bufferSize:")]
        EZAudioFloatData DataWithNumberOfChannels(int numberOfChannels, IntPtr buffers, uint bufferSize);

		// @property (readonly, assign, nonatomic) int numberOfChannels;
		[Export("numberOfChannels")]
		int NumberOfChannels { get; }

		// @property (readonly, assign, nonatomic) float ** buffers;
		[Export("buffers", ArgumentSemantic.Assign)]
		IntPtr Buffers { get; }

		// @property (readonly, assign, nonatomic) UInt32 bufferSize;
		[Export("bufferSize")]
		uint BufferSize { get; }

        //todo write wrapper: had float*
		// -(float *)bufferForChannel:(int)channel;
		[Export("bufferForChannel:")]
        IntPtr BufferForChannel(int channel);
	}

    //todo write wrapper: had float**
	// typedef void (^EZAudioWaveformDataCompletionBlock)(float **, int);
    delegate void EZAudioWaveformDataCompletionBlock(IntPtr waveformData, int length);

	// @protocol EZAudioFileDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface EZAudioFileDelegate
	{
        //todo write wrapper: had float**
		// @optional -(void)audioFile:(EZAudioFile *)audioFile readAudio:(float **)buffer withBufferSize:(UInt32)bufferSize withNumberOfChannels:(UInt32)numberOfChannels;
		[Export("audioFile:readAudio:withBufferSize:withNumberOfChannels:")]
		void AudioFile(EZAudioFile audioFile, IntPtr buffer, uint bufferSize, uint numberOfChannels);

		// @optional -(void)audioFileUpdatedPosition:(EZAudioFile *)audioFile;
		[Export("audioFileUpdatedPosition:")]
		void AudioFileUpdatedPosition(EZAudioFile audioFile);

		// @optional -(void)audioFile:(EZAudioFile *)audioFile updatedPosition:(SInt64)framePosition __attribute__((deprecated("")));
		[Export("audioFile:updatedPosition:")]
		void AudioFile(EZAudioFile audioFile, long framePosition);
	}

	// @interface EZAudioFile : NSObject <NSCopying>
	[BaseType(typeof(NSObject))]
	interface EZAudioFile : INSCopying
	{
		[Wrap("WeakDelegate")]
		EZAudioFileDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<EZAudioFileDelegate> delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(instancetype)initWithURL:(NSURL *)url;
		[Export("initWithURL:")]
		IntPtr Constructor(NSUrl url);

		// -(instancetype)initWithURL:(NSURL *)url delegate:(id<EZAudioFileDelegate>)delegate;
		[Export("initWithURL:delegate:")]
		IntPtr Constructor(NSUrl url, EZAudioFileDelegate @delegate);

		// -(instancetype)initWithURL:(NSURL *)url delegate:(id<EZAudioFileDelegate>)delegate clientFormat:(AudioStreamBasicDescription)clientFormat;
		[Export("initWithURL:delegate:clientFormat:")]
		IntPtr Constructor(NSUrl url, EZAudioFileDelegate @delegate, AudioStreamBasicDescription clientFormat);

		// +(instancetype)audioFileWithURL:(NSURL *)url;
		[Static]
		[Export("audioFileWithURL:")]
		EZAudioFile AudioFileWithURL(NSUrl url);

		// +(instancetype)audioFileWithURL:(NSURL *)url delegate:(id<EZAudioFileDelegate>)delegate;
		[Static]
		[Export("audioFileWithURL:delegate:")]
		EZAudioFile AudioFileWithURL(NSUrl url, EZAudioFileDelegate @delegate);

		// +(instancetype)audioFileWithURL:(NSURL *)url delegate:(id<EZAudioFileDelegate>)delegate clientFormat:(AudioStreamBasicDescription)clientFormat;
		[Static]
		[Export("audioFileWithURL:delegate:clientFormat:")]
		EZAudioFile AudioFileWithURL(NSUrl url, EZAudioFileDelegate @delegate, AudioStreamBasicDescription clientFormat);

        // +(AudioStreamBasicDescription)defaultClientFormat;
        [Static]
        [Export("defaultClientFormat")]
        AudioStreamBasicDescription DefaultClientFormat();

        // +(Float64)defaultClientFormatSampleRate;
        [Static]
        [Export("defaultClientFormatSampleRate")]
        double DefaultClientFormatSampleRate();

        // +(NSArray *)supportedAudioFileTypes;
        [Static]
        [Export("supportedAudioFileTypes")]
        NSString[] SupportedAudioFileTypes();

		// -(void)readFrames:(UInt32)frames audioBufferList:(AudioBufferList *)audioBufferList bufferSize:(UInt32 *)bufferSize eof:(BOOL *)eof;
		[Export("readFrames:audioBufferList:bufferSize:eof:")]
        void ReadFrames(uint frames, AudioBuffers audioBufferList, IntPtr bufferSize, IntPtr eof);

		// -(void)seekToFrame:(SInt64)frame;
		[Export("seekToFrame:")]
		void SeekToFrame(long frame);

		// @property (readwrite) AudioStreamBasicDescription clientFormat;
		[Export("clientFormat", ArgumentSemantic.Assign)]
		AudioStreamBasicDescription ClientFormat { get; set; }

		// @property (readwrite, nonatomic) NSTimeInterval currentTime;
		[Export("currentTime")]
		double CurrentTime { get; set; }

		// @property (readonly) NSTimeInterval duration;
		[Export("duration")]
		double Duration { get; }

		// @property (readonly) AudioStreamBasicDescription fileFormat;
		[Export("fileFormat")]
		AudioStreamBasicDescription FileFormat { get; }

		// @property (readonly) NSString * formattedCurrentTime;
		[Export("formattedCurrentTime")]
		string FormattedCurrentTime { get; }

		// @property (readonly) NSString * formattedDuration;
		[Export("formattedDuration")]
		string FormattedDuration { get; }

		// @property (readonly) SInt64 frameIndex;
		[Export("frameIndex")]
		long FrameIndex { get; }

		// @property (readonly) NSDictionary * metadata;
		[Export("metadata")]
		NSDictionary Metadata { get; }

		// @property (readonly) NSTimeInterval totalDuration __attribute__((deprecated("")));
		[Export("totalDuration")]
		double TotalDuration { get; }

		// @property (readonly) SInt64 totalClientFrames;
		[Export("totalClientFrames")]
		long TotalClientFrames { get; }

		// @property (readonly) SInt64 totalFrames;
		[Export("totalFrames")]
		long TotalFrames { get; }

		// @property (readonly, copy, nonatomic) NSURL * url;
		[Export("url", ArgumentSemantic.Copy)]
		NSUrl Url { get; }

        // -(EZAudioFloatData *)getWaveformData;
        [Export("getWaveformData")]
        EZAudioFloatData GetWaveformData();

		// -(EZAudioFloatData *)getWaveformDataWithNumberOfPoints:(UInt32)numberOfPoints;
		[Export("getWaveformDataWithNumberOfPoints:")]
		EZAudioFloatData GetWaveformDataWithNumberOfPoints(uint numberOfPoints);

		// -(void)getWaveformDataWithCompletionBlock:(EZAudioWaveformDataCompletionBlock)completion;
		[Export("getWaveformDataWithCompletionBlock:")]
		void GetWaveformDataWithCompletionBlock(EZAudioWaveformDataCompletionBlock completion);

		// -(void)getWaveformDataWithNumberOfPoints:(UInt32)numberOfPoints completion:(EZAudioWaveformDataCompletionBlock)completion;
		[Export("getWaveformDataWithNumberOfPoints:completion:")]
		void GetWaveformDataWithNumberOfPoints(uint numberOfPoints, EZAudioWaveformDataCompletionBlock completion);
	}

	// @protocol EZOutputDataSource <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface EZOutputDataSource
	{
		// @required -(OSStatus)output:(EZOutput *)output shouldFillAudioBufferList:(AudioBufferList *)audioBufferList withNumberOfFrames:(UInt32)frames timestamp:(const AudioTimeStamp *)timestamp;
		[Abstract]
		[Export("output:shouldFillAudioBufferList:withNumberOfFrames:timestamp:")]
        int ShouldFillAudioBufferList(EZOutput output, AudioBuffers audioBufferList, uint frames, IntPtr timestamp);
	}

	// @protocol EZOutputDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface EZOutputDelegate
	{
		// @optional -(void)output:(EZOutput *)output changedPlayingState:(BOOL)isPlaying;
		[Export("output:changedPlayingState:")]
		void ChangedPlayingState(EZOutput output, bool isPlaying);

		// @optional -(void)output:(EZOutput *)output changedDevice:(EZAudioDevice *)device;
		[Export("output:changedDevice:")]
		void ChangedDevice(EZOutput output, EZAudioDevice device);

		// @optional -(void)output:(EZOutput *)output playedAudio:(float **)buffer withBufferSize:(UInt32)bufferSize withNumberOfChannels:(UInt32)numberOfChannels;
		[Export("output:playedAudio:withBufferSize:withNumberOfChannels:")]
        void PlayedAudio(EZOutput output, IntPtr buffer, uint bufferSize, uint numberOfChannels);
	}

	// @interface EZOutput : NSObject
	[BaseType(typeof(NSObject))]
	interface EZOutput
	{
		// -(instancetype)initWithDataSource:(id<EZOutputDataSource>)dataSource;
		[Export("initWithDataSource:")]
		IntPtr Constructor(EZOutputDataSource dataSource);

		// -(instancetype)initWithDataSource:(id<EZOutputDataSource>)dataSource inputFormat:(AudioStreamBasicDescription)inputFormat;
		[Export("initWithDataSource:inputFormat:")]
		IntPtr Constructor(EZOutputDataSource dataSource, AudioStreamBasicDescription inputFormat);

		// +(instancetype)output;
		[Static]
		[Export("output")]
		EZOutput Output();

		// +(instancetype)outputWithDataSource:(id<EZOutputDataSource>)dataSource;
		[Static]
		[Export("outputWithDataSource:")]
		EZOutput OutputWithDataSource(EZOutputDataSource dataSource);

		// +(instancetype)outputWithDataSource:(id<EZOutputDataSource>)dataSource inputFormat:(AudioStreamBasicDescription)inputFormat;
		[Static]
		[Export("outputWithDataSource:inputFormat:")]
		EZOutput OutputWithDataSource(EZOutputDataSource dataSource, AudioStreamBasicDescription inputFormat);

		// +(instancetype)sharedOutput;
		[Static]
		[Export("sharedOutput")]
		EZOutput SharedOutput();

		// @property (readwrite, nonatomic) AudioStreamBasicDescription inputFormat;
		[Export("inputFormat", ArgumentSemantic.Assign)]
		AudioStreamBasicDescription InputFormat { get; set; }

		// @property (readwrite, nonatomic) AudioStreamBasicDescription clientFormat;
		[Export("clientFormat", ArgumentSemantic.Assign)]
		AudioStreamBasicDescription ClientFormat { get; set; }

		// @property (nonatomic, weak) id<EZOutputDataSource> dataSource;
		[Export("dataSource", ArgumentSemantic.Weak)]
		EZOutputDataSource DataSource { get; set; }

		[Wrap("WeakDelegate")]
		EZOutputDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<EZOutputDelegate> delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (readonly) BOOL isPlaying;
		[Export("isPlaying")]
		bool IsPlaying { get; }

		// @property (assign, nonatomic) float pan;
		[Export("pan")]
		float Pan { get; set; }

		// @property (assign, nonatomic) float volume;
		[Export("volume")]
		float Volume { get; set; }

        //todo look here later
		// @property (readonly) AUGraph graph;
		//[Export("graph")]
		//AUGraph Graph { get; }

        //fixme AudioUnit.AudioUnit doesn't work (3 below methods)
		// @property (readonly) AudioUnit converterAudioUnit;
		[Export("converterAudioUnit")]
        IntPtr ConverterAudioUnit { get; }

		// @property (readonly) AudioUnit mixerAudioUnit;
		[Export("mixerAudioUnit")]
        IntPtr MixerAudioUnit { get; }

		// @property (readonly) AudioUnit outputAudioUnit;
		[Export("outputAudioUnit")]
        IntPtr OutputAudioUnit { get; }

		// @property (readwrite, nonatomic, strong) EZAudioDevice * device;
		[Export("device", ArgumentSemantic.Strong)]
		EZAudioDevice Device { get; set; }

		// -(void)startPlayback;
		[Export("startPlayback")]
		void StartPlayback();

		// -(void)stopPlayback;
		[Export("stopPlayback")]
		void StopPlayback();

        //todo look here augraph
		// -(OSStatus)connectOutputOfSourceNode:(AUNode)sourceNode sourceNodeOutputBus:(UInt32)sourceNodeOutputBus toDestinationNode:(AUNode)destinationNode destinationNodeInputBus:(UInt32)destinationNodeInputBus inGraph:(AUGraph)graph;
		//[Export("connectOutputOfSourceNode:sourceNodeOutputBus:toDestinationNode:destinationNodeInputBus:inGraph:")]
        //int ConnectOutputOfSourceNode(int sourceNode, uint sourceNodeOutputBus, int destinationNode, uint destinationNodeInputBus, AUGraph graph);

        // -(AudioStreamBasicDescription)defaultClientFormat;
        [Export("defaultClientFormat")]
        AudioStreamBasicDescription DefaultClientFormat();

        // -(AudioStreamBasicDescription)defaultInputFormat;
        [Export("defaultInputFormat")]
        AudioStreamBasicDescription DefaultInputFormat();

        // -(OSType)outputAudioUnitSubType;
        [Export("outputAudioUnitSubType")]
        uint OutputAudioUnitSubType();
	}

	// @protocol EZMicrophoneDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface EZMicrophoneDelegate
	{
		// @optional -(void)microphone:(EZMicrophone *)microphone changedPlayingState:(BOOL)isPlaying;
		[Export("microphone:changedPlayingState:")]
		void ChangedPlayingState(EZMicrophone microphone, bool isPlaying);

		// @optional -(void)microphone:(EZMicrophone *)microphone changedDevice:(EZAudioDevice *)device;
		[Export("microphone:changedDevice:")]
		void ChangedDevice(EZMicrophone microphone, EZAudioDevice device);

		// @optional -(void)microphone:(EZMicrophone *)microphone hasAudioStreamBasicDescription:(AudioStreamBasicDescription)audioStreamBasicDescription;
		[Export("microphone:hasAudioStreamBasicDescription:")]
		void HasAudioStreamBasicDescription(EZMicrophone microphone, AudioStreamBasicDescription audioStreamBasicDescription);

		// @optional -(void)microphone:(EZMicrophone *)microphone hasAudioReceived:(float **)buffer withBufferSize:(UInt32)bufferSize withNumberOfChannels:(UInt32)numberOfChannels;
		[Export("microphone:hasAudioReceived:withBufferSize:withNumberOfChannels:")]
        void HasAudioReceived(EZMicrophone microphone, IntPtr buffer, uint bufferSize, uint numberOfChannels);

		// @optional -(void)microphone:(EZMicrophone *)microphone hasBufferList:(AudioBufferList *)bufferList withBufferSize:(UInt32)bufferSize withNumberOfChannels:(UInt32)numberOfChannels;
		[Export("microphone:hasBufferList:withBufferSize:withNumberOfChannels:")]
        void HasBufferList(EZMicrophone microphone, AudioBuffers bufferList, uint bufferSize, uint numberOfChannels);
	}

	// @interface EZMicrophone : NSObject <EZOutputDataSource>
	[BaseType(typeof(NSObject))]
	interface EZMicrophone : EZOutputDataSource
	{
		[Wrap("WeakDelegate")]
		EZMicrophoneDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<EZMicrophoneDelegate> delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (nonatomic, strong) EZAudioDevice * device;
		[Export("device", ArgumentSemantic.Strong)]
		EZAudioDevice Device { get; set; }

		// @property (assign, nonatomic) BOOL microphoneOn;
		[Export("microphoneOn")]
		bool MicrophoneOn { get; set; }

		// @property (nonatomic, strong) EZOutput * output;
		[Export("output", ArgumentSemantic.Strong)]
		EZOutput Output { get; set; }

		// -(EZMicrophone *)initWithMicrophoneDelegate:(id<EZMicrophoneDelegate>)delegate;
		[Export("initWithMicrophoneDelegate:")]
		IntPtr Constructor(EZMicrophoneDelegate @delegate);

		// -(EZMicrophone *)initWithMicrophoneDelegate:(id<EZMicrophoneDelegate>)delegate withAudioStreamBasicDescription:(AudioStreamBasicDescription)audioStreamBasicDescription;
		[Export("initWithMicrophoneDelegate:withAudioStreamBasicDescription:")]
		IntPtr Constructor(EZMicrophoneDelegate @delegate, AudioStreamBasicDescription audioStreamBasicDescription);

		// -(EZMicrophone *)initWithMicrophoneDelegate:(id<EZMicrophoneDelegate>)delegate startsImmediately:(BOOL)startsImmediately;
		[Export("initWithMicrophoneDelegate:startsImmediately:")]
		IntPtr Constructor(EZMicrophoneDelegate @delegate, bool startsImmediately);

		// -(EZMicrophone *)initWithMicrophoneDelegate:(id<EZMicrophoneDelegate>)delegate withAudioStreamBasicDescription:(AudioStreamBasicDescription)audioStreamBasicDescription startsImmediately:(BOOL)startsImmediately;
		[Export("initWithMicrophoneDelegate:withAudioStreamBasicDescription:startsImmediately:")]
		IntPtr Constructor(EZMicrophoneDelegate @delegate, AudioStreamBasicDescription audioStreamBasicDescription, bool startsImmediately);

		// +(EZMicrophone *)microphoneWithDelegate:(id<EZMicrophoneDelegate>)delegate;
		[Static]
		[Export("microphoneWithDelegate:")]
		EZMicrophone MicrophoneWithDelegate(EZMicrophoneDelegate @delegate);

		// +(EZMicrophone *)microphoneWithDelegate:(id<EZMicrophoneDelegate>)delegate withAudioStreamBasicDescription:(AudioStreamBasicDescription)audioStreamBasicDescription;
		[Static]
		[Export("microphoneWithDelegate:withAudioStreamBasicDescription:")]
		EZMicrophone MicrophoneWithDelegate(EZMicrophoneDelegate @delegate, AudioStreamBasicDescription audioStreamBasicDescription);

		// +(EZMicrophone *)microphoneWithDelegate:(id<EZMicrophoneDelegate>)delegate startsImmediately:(BOOL)startsImmediately;
		[Static]
		[Export("microphoneWithDelegate:startsImmediately:")]
		EZMicrophone MicrophoneWithDelegate(EZMicrophoneDelegate @delegate, bool startsImmediately);

		// +(EZMicrophone *)microphoneWithDelegate:(id<EZMicrophoneDelegate>)delegate withAudioStreamBasicDescription:(AudioStreamBasicDescription)audioStreamBasicDescription startsImmediately:(BOOL)startsImmediately;
		[Static]
		[Export("microphoneWithDelegate:withAudioStreamBasicDescription:startsImmediately:")]
		EZMicrophone MicrophoneWithDelegate(EZMicrophoneDelegate @delegate, AudioStreamBasicDescription audioStreamBasicDescription, bool startsImmediately);

        // +(EZMicrophone *)sharedMicrophone;
        [Static]
        [Export("sharedMicrophone")]
        EZMicrophone SharedMicrophone();

		// -(void)startFetchingAudio;
		[Export("startFetchingAudio")]
		void StartFetchingAudio();

		// -(void)stopFetchingAudio;
		[Export("stopFetchingAudio")]
		void StopFetchingAudio();

		// -(AudioStreamBasicDescription)audioStreamBasicDescription;
		// -(void)setAudioStreamBasicDescription:(AudioStreamBasicDescription)asbd;
		[Export("audioStreamBasicDescription")]
		//[Verify(MethodToProperty)]
        //todo check if this is right
		AudioStreamBasicDescription AudioStreamBasicDescription { get; set; }

        // -(AudioUnit *)audioUnit;
        [Export("audioUnit")]
        //todo write wrapper: had AudioUnit**
        IntPtr AudioUnit();

        // -(AudioStreamBasicDescription)defaultStreamFormat;
        [Export("defaultStreamFormat")]
        AudioStreamBasicDescription DefaultStreamFormat();

        // -(UInt32)numberOfChannels;
        [Export("numberOfChannels")]
        uint NumberOfChannels();
	}

	// @protocol EZRecorderDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface EZRecorderDelegate
	{
		// @optional -(void)recorderDidClose:(EZRecorder *)recorder;
		[Export("recorderDidClose:")]
		void RecorderDidClose(EZRecorder recorder);

		// @optional -(void)recorderUpdatedCurrentTime:(EZRecorder *)recorder;
		[Export("recorderUpdatedCurrentTime:")]
		void RecorderUpdatedCurrentTime(EZRecorder recorder);
	}

	// @interface EZRecorder : NSObject
	[BaseType(typeof(NSObject))]
	interface EZRecorder
	{
		[Wrap("WeakDelegate")]
		EZRecorderDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<EZRecorderDelegate> delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(instancetype)initWithURL:(NSURL *)url clientFormat:(AudioStreamBasicDescription)clientFormat fileType:(EZRecorderFileType)fileType;
		[Export("initWithURL:clientFormat:fileType:")]
		IntPtr Constructor(NSUrl url, AudioStreamBasicDescription clientFormat, EZRecorderFileType fileType);

		// -(instancetype)initWithURL:(NSURL *)url clientFormat:(AudioStreamBasicDescription)clientFormat fileType:(EZRecorderFileType)fileType delegate:(id<EZRecorderDelegate>)delegate;
		[Export("initWithURL:clientFormat:fileType:delegate:")]
		IntPtr Constructor(NSUrl url, AudioStreamBasicDescription clientFormat, EZRecorderFileType fileType, EZRecorderDelegate @delegate);

		// -(instancetype)initWithURL:(NSURL *)url clientFormat:(AudioStreamBasicDescription)clientFormat fileFormat:(AudioStreamBasicDescription)fileFormat audioFileTypeID:(AudioFileTypeID)audioFileTypeID;
		[Export("initWithURL:clientFormat:fileFormat:audioFileTypeID:")]
		IntPtr Constructor(NSUrl url, AudioStreamBasicDescription clientFormat, AudioStreamBasicDescription fileFormat, uint audioFileTypeID);

		// -(instancetype)initWithURL:(NSURL *)url clientFormat:(AudioStreamBasicDescription)clientFormat fileFormat:(AudioStreamBasicDescription)fileFormat audioFileTypeID:(AudioFileTypeID)audioFileTypeID delegate:(id<EZRecorderDelegate>)delegate;
		[Export("initWithURL:clientFormat:fileFormat:audioFileTypeID:delegate:")]
		IntPtr Constructor(NSUrl url, AudioStreamBasicDescription clientFormat, AudioStreamBasicDescription fileFormat, uint audioFileTypeID, EZRecorderDelegate @delegate);

        //Removed Constructor with same parameters
		// -(instancetype)initWithDestinationURL:(NSURL *)url sourceFormat:(AudioStreamBasicDescription)sourceFormat destinationFileType:(EZRecorderFileType)destinationFileType __attribute__((deprecated("")));
		//[Export("initWithDestinationURL:sourceFormat:destinationFileType:")]
		//IntPtr Constructor(NSUrl url, AudioStreamBasicDescription sourceFormat, EZRecorderFileType destinationFileType);

		// +(instancetype)recorderWithURL:(NSURL *)url clientFormat:(AudioStreamBasicDescription)clientFormat fileType:(EZRecorderFileType)fileType;
		[Static]
		[Export("recorderWithURL:clientFormat:fileType:")]
		EZRecorder RecorderWithURL(NSUrl url, AudioStreamBasicDescription clientFormat, EZRecorderFileType fileType);

		// +(instancetype)recorderWithURL:(NSURL *)url clientFormat:(AudioStreamBasicDescription)clientFormat fileType:(EZRecorderFileType)fileType delegate:(id<EZRecorderDelegate>)delegate;
		[Static]
		[Export("recorderWithURL:clientFormat:fileType:delegate:")]
		EZRecorder RecorderWithURL(NSUrl url, AudioStreamBasicDescription clientFormat, EZRecorderFileType fileType, EZRecorderDelegate @delegate);

		// +(instancetype)recorderWithURL:(NSURL *)url clientFormat:(AudioStreamBasicDescription)clientFormat fileFormat:(AudioStreamBasicDescription)fileFormat audioFileTypeID:(AudioFileTypeID)audioFileTypeID;
		[Static]
		[Export("recorderWithURL:clientFormat:fileFormat:audioFileTypeID:")]
		EZRecorder RecorderWithURL(NSUrl url, AudioStreamBasicDescription clientFormat, AudioStreamBasicDescription fileFormat, uint audioFileTypeID);

		// +(instancetype)recorderWithURL:(NSURL *)url clientFormat:(AudioStreamBasicDescription)clientFormat fileFormat:(AudioStreamBasicDescription)fileFormat audioFileTypeID:(AudioFileTypeID)audioFileTypeID delegate:(id<EZRecorderDelegate>)delegate;
		[Static]
		[Export("recorderWithURL:clientFormat:fileFormat:audioFileTypeID:delegate:")]
		EZRecorder RecorderWithURL(NSUrl url, AudioStreamBasicDescription clientFormat, AudioStreamBasicDescription fileFormat, uint audioFileTypeID, EZRecorderDelegate @delegate);

		// +(instancetype)recorderWithDestinationURL:(NSURL *)url sourceFormat:(AudioStreamBasicDescription)sourceFormat destinationFileType:(EZRecorderFileType)destinationFileType __attribute__((deprecated("")));
		[Static]
		[Export("recorderWithDestinationURL:sourceFormat:destinationFileType:")]
		EZRecorder RecorderWithDestinationURL(NSUrl url, AudioStreamBasicDescription sourceFormat, EZRecorderFileType destinationFileType);

		// @property (readwrite) AudioStreamBasicDescription clientFormat;
		[Export("clientFormat", ArgumentSemantic.Assign)]
		AudioStreamBasicDescription ClientFormat { get; set; }

		// @property (readonly) NSTimeInterval currentTime;
		[Export("currentTime")]
		double CurrentTime { get; }

		// @property (readonly) NSTimeInterval duration;
		[Export("duration")]
		double Duration { get; }

		// @property (readonly) AudioStreamBasicDescription fileFormat;
		[Export("fileFormat")]
		AudioStreamBasicDescription FileFormat { get; }

		// @property (readonly) NSString * formattedCurrentTime;
		[Export("formattedCurrentTime")]
		string FormattedCurrentTime { get; }

		// @property (readonly) NSString * formattedDuration;
		[Export("formattedDuration")]
		string FormattedDuration { get; }

		// @property (readonly) SInt64 frameIndex;
		[Export("frameIndex")]
		long FrameIndex { get; }

		// @property (readonly) SInt64 totalFrames;
		[Export("totalFrames")]
		long TotalFrames { get; }

        // -(NSURL *)url;
        [Export("url")]
        NSUrl Url();

		// -(void)appendDataFromBufferList:(AudioBufferList *)bufferList withBufferSize:(UInt32)bufferSize;
		[Export("appendDataFromBufferList:withBufferSize:")]
        void AppendDataFromBufferList(AudioBuffers bufferList, uint bufferSize);

		// -(void)closeAudioFile;
		[Export("closeAudioFile")]
		void CloseAudioFile();
	}

	// @protocol EZAudioPlayerDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface EZAudioPlayerDelegate
	{
        //todo write wrapper: had float**
		// @optional -(void)audioPlayer:(EZAudioPlayer *)audioPlayer playedAudio:(float **)buffer withBufferSize:(UInt32)bufferSize withNumberOfChannels:(UInt32)numberOfChannels inAudioFile:(EZAudioFile *)audioFile;
		[Export("audioPlayer:playedAudio:withBufferSize:withNumberOfChannels:inAudioFile:")]
        void PlayedAudio(EZAudioPlayer audioPlayer, IntPtr buffer, uint bufferSize, uint numberOfChannels, EZAudioFile audioFile);

		// @optional -(void)audioPlayer:(EZAudioPlayer *)audioPlayer updatedPosition:(SInt64)framePosition inAudioFile:(EZAudioFile *)audioFile;
		[Export("audioPlayer:updatedPosition:inAudioFile:")]
		void UpdatedPosition(EZAudioPlayer audioPlayer, long framePosition, EZAudioFile audioFile);

		// @optional -(void)audioPlayer:(EZAudioPlayer *)audioPlayer reachedEndOfAudioFile:(EZAudioFile *)audioFile;
		[Export("audioPlayer:reachedEndOfAudioFile:")]
		void ReachedEndOfAudioFile(EZAudioPlayer audioPlayer, EZAudioFile audioFile);
	}

	// @interface EZAudioPlayer : NSObject <EZAudioFileDelegate, EZOutputDataSource, EZOutputDelegate>
	[BaseType(typeof(NSObject))]
	interface EZAudioPlayer : EZAudioFileDelegate, EZOutputDataSource, EZOutputDelegate
	{
		[Wrap("WeakDelegate")]
		EZAudioPlayerDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<EZAudioPlayerDelegate> delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// @property (assign, nonatomic) BOOL shouldLoop;
		[Export("shouldLoop")]
		bool ShouldLoop { get; set; }

		// @property (readonly, assign, nonatomic) EZAudioPlayerState state;
		[Export("state", ArgumentSemantic.Assign)]
		EZAudioPlayerState State { get; }

		// -(instancetype)initWithAudioFile:(EZAudioFile *)audioFile;
		[Export("initWithAudioFile:")]
		IntPtr Constructor(EZAudioFile audioFile);

		// -(instancetype)initWithAudioFile:(EZAudioFile *)audioFile delegate:(id<EZAudioPlayerDelegate>)delegate;
		[Export("initWithAudioFile:delegate:")]
		IntPtr Constructor(EZAudioFile audioFile, EZAudioPlayerDelegate @delegate);

		// -(instancetype)initWithDelegate:(id<EZAudioPlayerDelegate>)delegate;
		[Export("initWithDelegate:")]
		IntPtr Constructor(EZAudioPlayerDelegate @delegate);

		// -(instancetype)initWithURL:(NSURL *)url;
		[Export("initWithURL:")]
		IntPtr Constructor(NSUrl url);

		// -(instancetype)initWithURL:(NSURL *)url delegate:(id<EZAudioPlayerDelegate>)delegate;
		[Export("initWithURL:delegate:")]
		IntPtr Constructor(NSUrl url, EZAudioPlayerDelegate @delegate);

		// +(instancetype)audioPlayer;
		[Static]
		[Export("audioPlayer")]
		EZAudioPlayer AudioPlayer();

		// +(instancetype)audioPlayerWithAudioFile:(EZAudioFile *)audioFile;
		[Static]
		[Export("audioPlayerWithAudioFile:")]
		EZAudioPlayer AudioPlayerWithAudioFile(EZAudioFile audioFile);

		// +(instancetype)audioPlayerWithAudioFile:(EZAudioFile *)audioFile delegate:(id<EZAudioPlayerDelegate>)delegate;
		[Static]
		[Export("audioPlayerWithAudioFile:delegate:")]
		EZAudioPlayer AudioPlayerWithAudioFile(EZAudioFile audioFile, EZAudioPlayerDelegate @delegate);

		// +(instancetype)audioPlayerWithDelegate:(id<EZAudioPlayerDelegate>)delegate;
		[Static]
		[Export("audioPlayerWithDelegate:")]
		EZAudioPlayer AudioPlayerWithDelegate(EZAudioPlayerDelegate @delegate);

		// +(instancetype)audioPlayerWithURL:(NSURL *)url;
		[Static]
		[Export("audioPlayerWithURL:")]
		EZAudioPlayer AudioPlayerWithURL(NSUrl url);

		// +(instancetype)audioPlayerWithURL:(NSURL *)url delegate:(id<EZAudioPlayerDelegate>)delegate;
		[Static]
		[Export("audioPlayerWithURL:delegate:")]
		EZAudioPlayer AudioPlayerWithURL(NSUrl url, EZAudioPlayerDelegate @delegate);

		// +(instancetype)sharedAudioPlayer;
		[Static]
		[Export("sharedAudioPlayer")]
		EZAudioPlayer SharedAudioPlayer();

        //fixme why are there 2 audioFiles in this class?
		// @property (readwrite, copy, nonatomic) EZAudioFile * audioFile;
		//[Export("audioFile", ArgumentSemantic.Copy)]
		//EZAudioFile AudioFile { get; set; }

		// @property (readwrite, nonatomic) NSTimeInterval currentTime;
		[Export("currentTime")]
		double CurrentTime { get; set; }

		// @property (readwrite) EZAudioDevice * device;
		[Export("device", ArgumentSemantic.Assign)]
		EZAudioDevice Device { get; set; }

		// @property (readonly) NSTimeInterval duration;
		[Export("duration")]
		double Duration { get; }

		// @property (readonly) NSString * formattedCurrentTime;
		[Export("formattedCurrentTime")]
		string FormattedCurrentTime { get; }

		// @property (readonly) NSString * formattedDuration;
		[Export("formattedDuration")]
		string FormattedDuration { get; }

		// @property (readwrite, nonatomic, strong) EZOutput * output;
		[Export("output", ArgumentSemantic.Strong)]
		EZOutput Output { get; set; }

		// @property (readonly) SInt64 frameIndex;
		[Export("frameIndex")]
		long FrameIndex { get; }

		// @property (readonly) BOOL isPlaying;
		[Export("isPlaying")]
		bool IsPlaying { get; }

		// @property (assign, nonatomic) float pan;
		[Export("pan")]
		float Pan { get; set; }

		// @property (readonly) SInt64 totalFrames;
		[Export("totalFrames")]
		long TotalFrames { get; }

		// @property (readonly, copy, nonatomic) NSURL * url;
		[Export("url", ArgumentSemantic.Copy)]
		NSUrl Url { get; }

		// @property (assign, nonatomic) float volume;
		[Export("volume")]
		float Volume { get; set; }

		// -(void)play;
		[Export("play")]
		void Play();

		// -(void)playAudioFile:(EZAudioFile *)audioFile;
		[Export("playAudioFile:")]
		void PlayAudioFile(EZAudioFile audioFile);

		// -(void)pause;
		[Export("pause")]
		void Pause();

		// -(void)seekToFrame:(SInt64)frame;
		[Export("seekToFrame:")]
		void SeekToFrame(long frame);
	}

	// @interface EZAudioUtilities : NSObject
	[BaseType(typeof(NSObject))]
	interface EZAudioUtilities
	{
		// +(BOOL)shouldExitOnCheckResultFail;
		// +(void)setShouldExitOnCheckResultFail:(BOOL)shouldExitOnCheckResultFail;
		[Static]
		[Export("shouldExitOnCheckResultFail")]
		//[Verify(MethodToProperty)]
        //todo check if this is right
		bool ShouldExitOnCheckResultFail { get; set; }

		// +(AudioBufferList *)audioBufferListWithNumberOfFrames:(UInt32)frames numberOfChannels:(UInt32)channels interleaved:(BOOL)interleaved;
		[Static]
		[Export("audioBufferListWithNumberOfFrames:numberOfChannels:interleaved:")]
        AudioBuffers AudioBufferListWithNumberOfFrames(uint frames, uint channels, bool interleaved);

		// +(float **)floatBuffersWithNumberOfFrames:(UInt32)frames numberOfChannels:(UInt32)channels;
		[Static]
		[Export("floatBuffersWithNumberOfFrames:numberOfChannels:")]
        IntPtr FloatBuffersWithNumberOfFrames(uint frames, uint channels);

		// +(void)freeBufferList:(AudioBufferList *)bufferList;
		[Static]
		[Export("freeBufferList:")]
		void FreeBufferList(AudioBuffers bufferList);

		// +(void)freeFloatBuffers:(float **)buffers numberOfChannels:(UInt32)channels;
		[Static]
		[Export("freeFloatBuffers:numberOfChannels:")]
        void FreeFloatBuffers(IntPtr buffers, uint channels);

		// +(AudioStreamBasicDescription)AIFFFormatWithNumberOfChannels:(UInt32)channels sampleRate:(float)sampleRate;
		[Static]
		[Export("AIFFFormatWithNumberOfChannels:sampleRate:")]
		AudioStreamBasicDescription AIFFFormatWithNumberOfChannels(uint channels, float sampleRate);

		// +(AudioStreamBasicDescription)iLBCFormatWithSampleRate:(float)sampleRate;
		[Static]
		[Export("iLBCFormatWithSampleRate:")]
		AudioStreamBasicDescription ILBCFormatWithSampleRate(float sampleRate);

		// +(AudioStreamBasicDescription)floatFormatWithNumberOfChannels:(UInt32)channels sampleRate:(float)sampleRate;
		[Static]
		[Export("floatFormatWithNumberOfChannels:sampleRate:")]
		AudioStreamBasicDescription FloatFormatWithNumberOfChannels(uint channels, float sampleRate);

		// +(AudioStreamBasicDescription)M4AFormatWithNumberOfChannels:(UInt32)channels sampleRate:(float)sampleRate;
		[Static]
		[Export("M4AFormatWithNumberOfChannels:sampleRate:")]
		AudioStreamBasicDescription M4AFormatWithNumberOfChannels(uint channels, float sampleRate);

		// +(AudioStreamBasicDescription)monoFloatFormatWithSampleRate:(float)sampleRate;
		[Static]
		[Export("monoFloatFormatWithSampleRate:")]
		AudioStreamBasicDescription MonoFloatFormatWithSampleRate(float sampleRate);

		// +(AudioStreamBasicDescription)monoCanonicalFormatWithSampleRate:(float)sampleRate;
		[Static]
		[Export("monoCanonicalFormatWithSampleRate:")]
		AudioStreamBasicDescription MonoCanonicalFormatWithSampleRate(float sampleRate);

		// +(AudioStreamBasicDescription)stereoCanonicalNonInterleavedFormatWithSampleRate:(float)sampleRate;
		[Static]
		[Export("stereoCanonicalNonInterleavedFormatWithSampleRate:")]
		AudioStreamBasicDescription StereoCanonicalNonInterleavedFormatWithSampleRate(float sampleRate);

		// +(AudioStreamBasicDescription)stereoFloatInterleavedFormatWithSampleRate:(float)sampleRate;
		[Static]
		[Export("stereoFloatInterleavedFormatWithSampleRate:")]
		AudioStreamBasicDescription StereoFloatInterleavedFormatWithSampleRate(float sampleRate);

		// +(AudioStreamBasicDescription)stereoFloatNonInterleavedFormatWithSampleRate:(float)sampleRate;
		[Static]
		[Export("stereoFloatNonInterleavedFormatWithSampleRate:")]
		AudioStreamBasicDescription StereoFloatNonInterleavedFormatWithSampleRate(float sampleRate);

		// +(BOOL)isFloatFormat:(AudioStreamBasicDescription)asbd;
		[Static]
		[Export("isFloatFormat:")]
		bool IsFloatFormat(AudioStreamBasicDescription asbd);

		// +(BOOL)isInterleaved:(AudioStreamBasicDescription)asbd;
		[Static]
		[Export("isInterleaved:")]
		bool IsInterleaved(AudioStreamBasicDescription asbd);

		// +(BOOL)isLinearPCM:(AudioStreamBasicDescription)asbd;
		[Static]
		[Export("isLinearPCM:")]
		bool IsLinearPCM(AudioStreamBasicDescription asbd);

		// +(void)printASBD:(AudioStreamBasicDescription)asbd;
		[Static]
		[Export("printASBD:")]
		void PrintASBD(AudioStreamBasicDescription asbd);

		// +(NSString *)displayTimeStringFromSeconds:(NSTimeInterval)seconds;
		[Static]
		[Export("displayTimeStringFromSeconds:")]
		string DisplayTimeStringFromSeconds(double seconds);

		// +(NSString *)stringForAudioStreamBasicDescription:(AudioStreamBasicDescription)asbd;
		[Static]
		[Export("stringForAudioStreamBasicDescription:")]
		string StringForAudioStreamBasicDescription(AudioStreamBasicDescription asbd);

		//todo Was AudioStreamBasicDescription *
		// +(void)setCanonicalAudioStreamBasicDescription:(AudioStreamBasicDescription *)asbd numberOfChannels:(UInt32)nChannels interleaved:(BOOL)interleaved;
		[Static]
		[Export("setCanonicalAudioStreamBasicDescription:numberOfChannels:interleaved:")]
        void SetCanonicalAudioStreamBasicDescription(IntPtr asbd, uint nChannels, bool interleaved);

        //todo was float*
		// +(void)appendBufferAndShift:(float *)buffer withBufferSize:(int)bufferLength toScrollHistory:(float *)scrollHistory withScrollHistorySize:(int)scrollHistoryLength;
		[Static]
		[Export("appendBufferAndShift:withBufferSize:toScrollHistory:withScrollHistorySize:")]
        void AppendBufferAndShift(IntPtr buffer, int bufferLength, IntPtr scrollHistory, int scrollHistoryLength);

		// +(void)appendValue:(float)value toScrollHistory:(float *)scrollHistory withScrollHistorySize:(int)scrollHistoryLength;
		[Static]
		[Export("appendValue:toScrollHistory:withScrollHistorySize:")]
        void AppendValue(float value, IntPtr scrollHistory, int scrollHistoryLength);

		// +(float)MAP:(float)value leftMin:(float)leftMin leftMax:(float)leftMax rightMin:(float)rightMin rightMax:(float)rightMax;
		[Static]
		[Export("MAP:leftMin:leftMax:rightMin:rightMax:")]
		float MAP(float value, float leftMin, float leftMax, float rightMin, float rightMax);

        //todo write wrapper: had float*
		// +(float)RMS:(float *)buffer length:(int)bufferSize;
		[Static]
		[Export("RMS:length:")]
        float RMS(IntPtr buffer, int bufferSize);

		// +(float)SGN:(float)value;
		[Static]
		[Export("SGN:")]
		float SGN(float value);

		// +(NSString *)noteNameStringForFrequency:(float)frequency includeOctave:(BOOL)includeOctave;
		[Static]
		[Export("noteNameStringForFrequency:includeOctave:")]
		string NoteNameStringForFrequency(float frequency, bool includeOctave);

		// +(void)checkResult:(OSStatus)result operation:(const char *)operation;
		[Static]
		[Export("checkResult:operation:")]
        void CheckResult(int result, IntPtr operation);

		// +(NSString *)stringFromUInt32Code:(UInt32)code;
		[Static]
		[Export("stringFromUInt32Code:")]
		string StringFromUInt32Code(uint code);

        //todo write wrapper: had CGColorRef*, nfloat* (multiple)
		// +(void)getColorComponentsFromCGColor:(CGColorRef)color red:(CGFloat *)red green:(CGFloat *)green blue:(CGFloat *)blue alpha:(CGFloat *)alpha;
		[Static]
		[Export("getColorComponentsFromCGColor:red:green:blue:alpha:")]
        void GetColorComponentsFromCGColor(IntPtr color, IntPtr red, IntPtr green, IntPtr blue, IntPtr alpha);

        //todo write wrapper: had float** int* float* bool*
		// +(void)updateScrollHistory:(float **)scrollHistory withLength:(int)scrollHistoryLength atIndex:(int *)index withBuffer:(float *)buffer withBufferSize:(int)bufferSize isResolutionChanging:(BOOL *)isChanging;
		[Static]
		[Export("updateScrollHistory:withLength:atIndex:withBuffer:withBufferSize:isResolutionChanging:")]
        void UpdateScrollHistory(IntPtr scrollHistory, int scrollHistoryLength, IntPtr index, IntPtr buffer, int bufferSize, IntPtr isChanging);

        //todo write wrapper: had TPCircularBuffer*
		// +(void)appendDataToCircularBuffer:(TPCircularBuffer *)circularBuffer fromAudioBufferList:(AudioBufferList *)audioBufferList;
		[Static]
		[Export("appendDataToCircularBuffer:fromAudioBufferList:")]
        void AppendDataToCircularBuffer(IntPtr circularBuffer, AudioBuffers audioBufferList);

		//todo write wrapper: had TPCircularBuffer*
		// +(void)circularBuffer:(TPCircularBuffer *)circularBuffer withSize:(int)size;
		[Static]
		[Export("circularBuffer:withSize:")]
		void CircularBuffer(IntPtr circularBuffer, int size);

		//todo write wrapper: had TPCircularBuffer*
		// +(void)freeCircularBuffer:(TPCircularBuffer *)circularBuffer;
		[Static]
		[Export("freeCircularBuffer:")]
        void FreeCircularBuffer(IntPtr circularBuffer);

        //todo write wrapper: had float*, EZPlotHistoryInfo*
		// +(void)appendBufferRMS:(float *)buffer withBufferSize:(UInt32)bufferSize toHistoryInfo:(EZPlotHistoryInfo *)historyInfo;
		[Static]
		[Export("appendBufferRMS:withBufferSize:toHistoryInfo:")]
        void AppendBufferRMS(IntPtr buffer, uint bufferSize, IntPtr historyInfo);

		//todo write wrapper: had float*, EZPlotHistoryInfo*
		// +(void)appendBuffer:(float *)buffer withBufferSize:(UInt32)bufferSize toHistoryInfo:(EZPlotHistoryInfo *)historyInfo;
		[Static]
		[Export("appendBuffer:withBufferSize:toHistoryInfo:")]
        void AppendBuffer(IntPtr buffer, uint bufferSize, IntPtr historyInfo);

        //todo write wrapper: had EZPlotHistoryInfo*
		// +(void)clearHistoryInfo:(EZPlotHistoryInfo *)historyInfo;
		[Static]
		[Export("clearHistoryInfo:")]
        void ClearHistoryInfo(IntPtr historyInfo);

        //todo write wrapper: had EZPlotHistoryInfo*
		// +(void)freeHistoryInfo:(EZPlotHistoryInfo *)historyInfo;
		[Static]
		[Export("freeHistoryInfo:")]
        void FreeHistoryInfo(IntPtr historyInfo);

		//todo write wrapper: had EZPlotHistoryInfo*
		// +(EZPlotHistoryInfo *)historyInfoWithDefaultLength:(int)defaultLength maximumLength:(int)maximumLength;
		[Static]
		[Export("historyInfoWithDefaultLength:maximumLength:")]
        IntPtr HistoryInfoWithDefaultLength(int defaultLength, int maximumLength);
	}

	// @interface EZPlot : UIView
	[BaseType(typeof(UIView))]
	interface EZPlot
	{
		// @property (nonatomic, strong) UIColor * backgroundColor;
		[Export("backgroundColor", ArgumentSemantic.Strong)]
		UIColor BackgroundColor { get; set; }

		// @property (nonatomic, strong) UIColor * color;
		[Export("color", ArgumentSemantic.Strong)]
		UIColor Color { get; set; }

		// @property (assign, nonatomic) float gain;
		[Export("gain")]
		float Gain { get; set; }

		// @property (assign, nonatomic) EZPlotType plotType;
		[Export("plotType", ArgumentSemantic.Assign)]
		EZPlotType PlotType { get; set; }

		// @property (assign, nonatomic) BOOL shouldFill;
		[Export("shouldFill")]
		bool ShouldFill { get; set; }

		// @property (assign, nonatomic) BOOL shouldMirror;
		[Export("shouldMirror")]
		bool ShouldMirror { get; set; }

		// -(void)clear;
		[Export("clear")]
		void Clear();

        //todo write wrapper: had float*
		// -(void)updateBuffer:(float *)buffer withBufferSize:(UInt32)bufferSize;
		[Export("updateBuffer:withBufferSize:")]
        void UpdateBuffer(IntPtr buffer, uint bufferSize);
	}

	// @protocol EZAudioDisplayLinkDelegate <NSObject>
	[Protocol, Model]
	[BaseType(typeof(NSObject))]
	interface EZAudioDisplayLinkDelegate
	{
		// @required -(void)displayLinkNeedsDisplay:(EZAudioDisplayLink *)displayLink;
		[Abstract]
		[Export("displayLinkNeedsDisplay:")]
		void DisplayLinkNeedsDisplay(EZAudioDisplayLink displayLink);
	}

	// @interface EZAudioDisplayLink : NSObject
	[BaseType(typeof(NSObject))]
	interface EZAudioDisplayLink
	{
		// +(instancetype)displayLinkWithDelegate:(id<EZAudioDisplayLinkDelegate>)delegate;
		[Static]
		[Export("displayLinkWithDelegate:")]
		EZAudioDisplayLink DisplayLinkWithDelegate(EZAudioDisplayLinkDelegate @delegate);

		[Wrap("WeakDelegate")]
		EZAudioDisplayLinkDelegate Delegate { get; set; }

		// @property (nonatomic, weak) id<EZAudioDisplayLinkDelegate> delegate;
		[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
		NSObject WeakDelegate { get; set; }

		// -(void)start;
		[Export("start")]
		void Start();

		// -(void)stop;
		[Export("stop")]
		void Stop();
	}

	// @interface EZAudioPlotWaveformLayer : CAShapeLayer
	[BaseType(typeof(CAShapeLayer))]
	interface EZAudioPlotWaveformLayer
	{
	}

	// @interface EZAudioPlot : EZPlot
	[BaseType(typeof(EZPlot))]
	interface EZAudioPlot : EZAudioDisplayLinkDelegate
	{
		// @property (assign, nonatomic) BOOL shouldOptimizeForRealtimePlot;
		[Export("shouldOptimizeForRealtimePlot")]
		bool ShouldOptimizeForRealtimePlot { get; set; }

		// @property (assign, nonatomic) BOOL shouldCenterYAxis;
		[Export("shouldCenterYAxis")]
		bool ShouldCenterYAxis { get; set; }

		// @property (nonatomic, strong) EZAudioPlotWaveformLayer * waveformLayer;
		[Export("waveformLayer", ArgumentSemantic.Strong)]
		EZAudioPlotWaveformLayer WaveformLayer { get; set; }

		// -(int)setRollingHistoryLength:(int)historyLength;
		[Export("setRollingHistoryLength:")]
		int SetRollingHistoryLength(int historyLength);

        // -(int)rollingHistoryLength;
        [Export("rollingHistoryLength")]
        int RollingHistoryLength();

        //todo write wrapper: had CGPathRef*, EZRect (EZRect is a CGRect on phone)
		// -(CGPathRef)createPathWithPoints:(CGPoint *)points pointCount:(UInt32)pointCount inRect:(EZRect)rect;
		[Export("createPathWithPoints:pointCount:inRect:")]
        IntPtr CreatePathWithPoints(IntPtr points, uint pointCount, CGRect rect);

        // -(int)defaultRollingHistoryLength;
        [Export("defaultRollingHistoryLength")]
        int DefaultRollingHistoryLength();

		// -(void)setupPlot;
		[Export("setupPlot")]
		void SetupPlot();

        // -(int)initialPointCount;
        [Export("initialPointCount")]
        int InitialPointCount();

        // -(int)maximumRollingHistoryLength;
        [Export("maximumRollingHistoryLength")]
        int MaximumRollingHistoryLength();

		// -(void)redraw;
		[Export("redraw")]
		void Redraw();

		// -(void)setSampleData:(float *)data length:(int)length;
		[Export("setSampleData:length:")]
        void SetSampleData(IntPtr data, int length);

		// @property (nonatomic, strong) EZAudioDisplayLink * displayLink;
		[Export("displayLink", ArgumentSemantic.Strong)]
		EZAudioDisplayLink DisplayLink { get; set; }

		//todo write wrapper: had EZPlotHistoryInfo*
		// @property (assign, nonatomic) EZPlotHistoryInfo * historyInfo;
		[Export("historyInfo", ArgumentSemantic.Assign)]
        IntPtr HistoryInfo { get; set; }

        //todo write wrapper: had CGPoint*
		// @property (assign, nonatomic) CGPoint * points;
		[Export("points", ArgumentSemantic.Assign)]
        IntPtr Points { get; set; }

		// @property (assign, nonatomic) UInt32 pointCount;
		[Export("pointCount")]
		uint PointCount { get; set; }
	}

    #region Extension
    /*
    // @interface  (EZAudioPlot) <EZAudioDisplayLinkDelegate>
    [Category]
	[BaseType(typeof(EZAudioPlot))]
	interface EZAudioPlot_ : EZAudioDisplayLinkDelegate
	{
		// @property (nonatomic, strong) EZAudioDisplayLink * displayLink;
		[Export("displayLink", ArgumentSemantic.Strong)]
		EZAudioDisplayLink DisplayLink { get; set; }

		//todo write wrapper: had EZPlotHistoryInfo*
		// @property (assign, nonatomic) EZPlotHistoryInfo * historyInfo;
		[Export("historyInfo", ArgumentSemantic.Assign)]
        IntPtr HistoryInfo { get; set; }

        //todo write wrapper: had CGPoint*
		// @property (assign, nonatomic) CGPoint * points;
		[Export("points", ArgumentSemantic.Assign)]
        IntPtr Points { get; set; }

		// @property (assign, nonatomic) UInt32 pointCount;
		[Export("pointCount")]
		uint PointCount { get; set; }
	}*/
    #endregion

    // @interface EZAudioPlotGL : GLKView
    [BaseType(typeof(GLKView))]
	interface EZAudioPlotGL
	{
		// @property (nonatomic, strong) UIColor * backgroundColor;
		[Export("backgroundColor", ArgumentSemantic.Strong)]
		UIColor BackgroundColor { get; set; }

		// @property (nonatomic, strong) UIColor * color;
		[Export("color", ArgumentSemantic.Strong)]
		UIColor Color { get; set; }

		// @property (assign, nonatomic) float gain;
		[Export("gain")]
		float Gain { get; set; }

		// @property (assign, nonatomic) EZPlotType plotType;
		[Export("plotType", ArgumentSemantic.Assign)]
		EZPlotType PlotType { get; set; }

		// @property (assign, nonatomic) BOOL shouldFill;
		[Export("shouldFill")]
		bool ShouldFill { get; set; }

		// @property (assign, nonatomic) BOOL shouldMirror;
		[Export("shouldMirror")]
		bool ShouldMirror { get; set; }

        //todo write wrapper: had float* [DONE]
		// -(void)updateBuffer:(float *)buffer withBufferSize:(UInt32)bufferSize;
		[Export("updateBuffer:withBufferSize:")]//[Internal]
        void UpdateBuffer(IntPtr buffer, uint bufferSize);

		// -(int)setRollingHistoryLength:(int)historyLength;
		[Export("setRollingHistoryLength:")]
		int SetRollingHistoryLength(int historyLength);

        // -(int)rollingHistoryLength;
        [Export("rollingHistoryLength")]
        int RollingHistoryLength();

		// -(void)clear;
		[Export("clear")]
		void Clear();

		// -(void)pauseDrawing;
		[Export("pauseDrawing")]
		void PauseDrawing();

		// -(void)resumeDrawing;
		[Export("resumeDrawing")]
		void ResumeDrawing();

        //todo write wrapper: had EZAudioPlotGLPoint*
		// -(void)redrawWithPoints:(EZAudioPlotGLPoint *)points pointCount:(UInt32)pointCount baseEffect:(GLKBaseEffect *)baseEffect vertexBufferObject:(GLuint)vbo vertexArrayBuffer:(GLuint)vab interpolated:(BOOL)interpolated mirrored:(BOOL)mirrored gain:(float)gain;
		[Export("redrawWithPoints:pointCount:baseEffect:vertexBufferObject:vertexArrayBuffer:interpolated:mirrored:gain:")]
        void RedrawWithPoints(IntPtr points, uint pointCount, GLKBaseEffect baseEffect, uint vbo, uint vab, bool interpolated, bool mirrored, float gain);

		// -(void)redraw;
		[Export("redraw")]
		void Redraw();

		// -(void)setup;
		[Export("setup")]
		void Setup();

        //todo write wrapper: had float*
		// -(void)setSampleData:(float *)data length:(int)length;
		[Export("setSampleData:length:")]
        void SetSampleData(IntPtr data, int length);

        // -(int)defaultRollingHistoryLength;
        [Export("defaultRollingHistoryLength")]
        int DefaultRollingHistoryLength();

        // -(int)maximumRollingHistoryLength;
        [Export("maximumRollingHistoryLength")]
        int MaximumRollingHistoryLength();
	}

    /* Accelerate framework not fully supported
	//// @protocol EZAudioFFTDelegate <NSObject>
	//[Protocol, Model]
	//[BaseType(typeof(NSObject))]
	//interface EZAudioFFTDelegate
	//{
 //       //todo write wrapper: had float*
	//	// @optional -(void)fft:(EZAudioFFT *)fft updatedWithFFTData:(float *)fftData bufferSize:(vDSP_Length)bufferSize;
	//	[Export("fft:updatedWithFFTData:bufferSize:")]ee
 //       void UpdatedWithFFTData(EZAudioFFT fft, IntPtr fftData, nuint bufferSize);
	//}


	//// @interface EZAudioFFT : NSObject
	//[BaseType(typeof(NSObject))]
	//interface EZAudioFFT
	//{
	//	// -(instancetype)initWithMaximumBufferSize:(vDSP_Length)maximumBufferSize sampleRate:(float)sampleRate;
	//	[Export("initWithMaximumBufferSize:sampleRate:")]
	//	IntPtr Constructor(nuint maximumBufferSize, float sampleRate);

	//	// -(instancetype)initWithMaximumBufferSize:(vDSP_Length)maximumBufferSize sampleRate:(float)sampleRate delegate:(id<EZAudioFFTDelegate>)delegate;
	//	[Export("initWithMaximumBufferSize:sampleRate:delegate:")]
	//	IntPtr Constructor(nuint maximumBufferSize, float sampleRate, EZAudioFFTDelegate @delegate);

	//	// +(instancetype)fftWithMaximumBufferSize:(vDSP_Length)maximumBufferSize sampleRate:(float)sampleRate;
	//	[Static]
	//	[Export("fftWithMaximumBufferSize:sampleRate:")]
	//	EZAudioFFT FftWithMaximumBufferSize(nuint maximumBufferSize, float sampleRate);

	//	// +(instancetype)fftWithMaximumBufferSize:(vDSP_Length)maximumBufferSize sampleRate:(float)sampleRate delegate:(id<EZAudioFFTDelegate>)delegate;
	//	[Static]
	//	[Export("fftWithMaximumBufferSize:sampleRate:delegate:")]
	//	EZAudioFFT FftWithMaximumBufferSize(nuint maximumBufferSize, float sampleRate, EZAudioFFTDelegate @delegate);

	//	[Wrap("WeakDelegate")]
	//	EZAudioFFTDelegate Delegate { get; set; }

	//	// @property (nonatomic, weak) id<EZAudioFFTDelegate> delegate;
	//	[NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
	//	NSObject WeakDelegate { get; set; }

	//	// @property (readonly, nonatomic) COMPLEX_SPLIT complexSplit;
	//	[Export("complexSplit")]
	//	COMPLEX_SPLIT ComplexSplit { get; }

 //       //todo write wrapper: had float*
	//	// @property (readonly, nonatomic) float * fftData;
	//	[Export("fftData")]
 //       IntPtr FftData { get; }

	//	// @property (readonly, nonatomic) FFTSetup fftSetup;
	//	[Export("fftSetup")]
	//	unsafe FFTSetup* FftSetup { get; }

 //       //todo write wrapper: had float*
	//	// @property (readonly, nonatomic) float * inversedFFTData;
	//	[Export("inversedFFTData")]
 //       IntPtr InversedFFTData { get; }

	//	// @property (readonly, nonatomic) float maxFrequency;
	//	[Export("maxFrequency")]
	//	float MaxFrequency { get; }

	//	// @property (readonly, nonatomic) vDSP_Length maxFrequencyIndex;
	//	[Export("maxFrequencyIndex")]
	//	nuint MaxFrequencyIndex { get; }

	//	// @property (readonly, nonatomic) float maxFrequencyMagnitude;
	//	[Export("maxFrequencyMagnitude")]
	//	float MaxFrequencyMagnitude { get; }

	//	// @property (readonly, nonatomic) vDSP_Length maximumBufferSize;
	//	[Export("maximumBufferSize")]
	//	nuint MaximumBufferSize { get; }

	//	// @property (readwrite, nonatomic) float sampleRate;
	//	[Export("sampleRate")]
	//	float SampleRate { get; set; }

 //       //todo write wrapper: 2 float*
	//	// -(float *)computeFFTWithBuffer:(float *)buffer withBufferSize:(UInt32)bufferSize;
	//	[Export("computeFFTWithBuffer:withBufferSize:")]
 //       IntPtr ComputeFFTWithBuffer(IntPtr buffer, uint bufferSize);

	//	// -(float)frequencyAtIndex:(vDSP_Length)index;
	//	[Export("frequencyAtIndex:")]
	//	float FrequencyAtIndex(nuint index);

	//	// -(float)frequencyMagnitudeAtIndex:(vDSP_Length)index;
	//	[Export("frequencyMagnitudeAtIndex:")]
	//	float FrequencyMagnitudeAtIndex(nuint index);
	//}
    //
	//// @interface EZAudioFFTRolling : EZAudioFFT
	//[BaseType(typeof(EZAudioFFT))]
	//interface EZAudioFFTRolling
	//{
	//	// -(instancetype)initWithWindowSize:(vDSP_Length)windowSize sampleRate:(float)sampleRate;
	//	[Export("initWithWindowSize:sampleRate:")]
	//	IntPtr Constructor(nuint windowSize, float sampleRate);

	//	// -(instancetype)initWithWindowSize:(vDSP_Length)windowSize sampleRate:(float)sampleRate delegate:(id<EZAudioFFTDelegate>)delegate;
	//	[Export("initWithWindowSize:sampleRate:delegate:")]
	//	IntPtr Constructor(nuint windowSize, float sampleRate, EZAudioFFTDelegate @delegate);

	//	// -(instancetype)initWithWindowSize:(vDSP_Length)windowSize historyBufferSize:(vDSP_Length)historyBufferSize sampleRate:(float)sampleRate;
	//	[Export("initWithWindowSize:historyBufferSize:sampleRate:")]
	//	IntPtr Constructor(nuint windowSize, nuint historyBufferSize, float sampleRate);

	//	// -(instancetype)initWithWindowSize:(vDSP_Length)windowSize historyBufferSize:(vDSP_Length)historyBufferSize sampleRate:(float)sampleRate delegate:(id<EZAudioFFTDelegate>)delegate;
	//	[Export("initWithWindowSize:historyBufferSize:sampleRate:delegate:")]
	//	IntPtr Constructor(nuint windowSize, nuint historyBufferSize, float sampleRate, EZAudioFFTDelegate @delegate);

	//	// +(instancetype)fftWithWindowSize:(vDSP_Length)windowSize sampleRate:(float)sampleRate;
	//	[Static]
	//	[Export("fftWithWindowSize:sampleRate:")]
	//	EZAudioFFTRolling FftWithWindowSize(nuint windowSize, float sampleRate);

	//	// +(instancetype)fftWithWindowSize:(vDSP_Length)windowSize sampleRate:(float)sampleRate delegate:(id<EZAudioFFTDelegate>)delegate;
	//	[Static]
	//	[Export("fftWithWindowSize:sampleRate:delegate:")]
	//	EZAudioFFTRolling FftWithWindowSize(nuint windowSize, float sampleRate, EZAudioFFTDelegate @delegate);

	//	// +(instancetype)fftWithWindowSize:(vDSP_Length)windowSize historyBufferSize:(vDSP_Length)historyBufferSize sampleRate:(float)sampleRate;
	//	[Static]
	//	[Export("fftWithWindowSize:historyBufferSize:sampleRate:")]
	//	EZAudioFFTRolling FftWithWindowSize(nuint windowSize, nuint historyBufferSize, float sampleRate);

	//	// +(instancetype)fftWithWindowSize:(vDSP_Length)windowSize historyBufferSize:(vDSP_Length)historyBufferSize sampleRate:(float)sampleRate delegate:(id<EZAudioFFTDelegate>)delegate;
	//	[Static]
	//	[Export("fftWithWindowSize:historyBufferSize:sampleRate:delegate:")]
	//	EZAudioFFTRolling FftWithWindowSize(nuint windowSize, nuint historyBufferSize, float sampleRate, EZAudioFFTDelegate @delegate);

	//	// @property (readonly, nonatomic) vDSP_Length windowSize;
	//	[Export("windowSize")]
	//	nuint WindowSize { get; }

	//	// @property (readonly, nonatomic) float * timeDomainData;
	//	[Export("timeDomainData")]
 //       IntPtr TimeDomainData { get; }

	//	// @property (readonly, nonatomic) UInt32 timeDomainBufferSize;
	//	[Export("timeDomainBufferSize")]
	//	uint TimeDomainBufferSize { get; }
	//}
    */

	// @interface EZAudioFloatConverter : NSObject
	[BaseType(typeof(NSObject))]
	interface EZAudioFloatConverter
	{
		// +(instancetype)converterWithInputFormat:(AudioStreamBasicDescription)inputFormat;
		[Static]
		[Export("converterWithInputFormat:")]
		EZAudioFloatConverter ConverterWithInputFormat(AudioStreamBasicDescription inputFormat);

		// @property (readonly, assign, nonatomic) AudioStreamBasicDescription inputFormat;
		[Export("inputFormat", ArgumentSemantic.Assign)]
		AudioStreamBasicDescription InputFormat { get; }

		// @property (readonly, assign, nonatomic) AudioStreamBasicDescription floatFormat;
		[Export("floatFormat", ArgumentSemantic.Assign)]
		AudioStreamBasicDescription FloatFormat { get; }

		// -(instancetype)initWithInputFormat:(AudioStreamBasicDescription)inputFormat;
		[Export("initWithInputFormat:")]
		IntPtr Constructor(AudioStreamBasicDescription inputFormat);

		// -(void)convertDataFromAudioBufferList:(AudioBufferList *)audioBufferList withNumberOfFrames:(UInt32)frames toFloatBuffers:(float **)buffers;
		[Export("convertDataFromAudioBufferList:withNumberOfFrames:toFloatBuffers:")]
        void ConvertDataFromAudioBufferList(AudioBuffers audioBufferList, uint frames, IntPtr buffers);

		// -(void)convertDataFromAudioBufferList:(AudioBufferList *)audioBufferList withNumberOfFrames:(UInt32)frames toFloatBuffers:(float **)buffers packetDescriptions:(AudioStreamPacketDescription *)packetDescriptions;
		[Export("convertDataFromAudioBufferList:withNumberOfFrames:toFloatBuffers:packetDescriptions:")]
        void ConvertDataFromAudioBufferList(AudioBuffers audioBufferList, uint frames, IntPtr buffers, IntPtr packetDescriptions);
	}

	//todo look at all the methods and properties with TPCircularBuffer *
	// @interface EZAudio : NSObject
	[BaseType(typeof(NSObject))]
	interface EZAudio
	{
		// +(BOOL)shouldExitOnCheckResultFail __attribute__((deprecated("")));
		// +(void)setShouldExitOnCheckResultFail:(BOOL)shouldExitOnCheckResultFail __attribute__((deprecated("")));
		[Static]
		[Export("shouldExitOnCheckResultFail")]
		//[Verify(MethodToProperty)]
        //todo check if this is right
		bool ShouldExitOnCheckResultFail { get; set; }

		// +(AudioBufferList *)audioBufferListWithNumberOfFrames:(UInt32)frames numberOfChannels:(UInt32)channels interleaved:(BOOL)interleaved __attribute__((deprecated("")));
		[Static]
		[Export("audioBufferListWithNumberOfFrames:numberOfChannels:interleaved:")]
        AudioBuffers AudioBufferListWithNumberOfFrames(uint frames, uint channels, bool interleaved);

		// +(float **)floatBuffersWithNumberOfFrames:(UInt32)frames numberOfChannels:(UInt32)channels __attribute__((deprecated("")));
		[Static]
		[Export("floatBuffersWithNumberOfFrames:numberOfChannels:")]
        IntPtr FloatBuffersWithNumberOfFrames(uint frames, uint channels);

		// +(void)freeBufferList:(AudioBufferList *)bufferList __attribute__((deprecated("")));
		[Static]
		[Export("freeBufferList:")]
		void FreeBufferList(AudioBuffers bufferList);

		// +(void)freeFloatBuffers:(float **)buffers numberOfChannels:(UInt32)channels __attribute__((deprecated("")));
		[Static]
		[Export("freeFloatBuffers:numberOfChannels:")]
        void FreeFloatBuffers(IntPtr buffers, uint channels);

		// +(AudioStreamBasicDescription)AIFFFormatWithNumberOfChannels:(UInt32)channels sampleRate:(float)sampleRate __attribute__((deprecated("")));
		[Static]
		[Export("AIFFFormatWithNumberOfChannels:sampleRate:")]
		AudioStreamBasicDescription AIFFFormatWithNumberOfChannels(uint channels, float sampleRate);

		// +(AudioStreamBasicDescription)iLBCFormatWithSampleRate:(float)sampleRate __attribute__((deprecated("")));
		[Static]
		[Export("iLBCFormatWithSampleRate:")]
		AudioStreamBasicDescription ILBCFormatWithSampleRate(float sampleRate);

		// +(AudioStreamBasicDescription)floatFormatWithNumberOfChannels:(UInt32)channels sampleRate:(float)sampleRate __attribute__((deprecated("")));
		[Static]
		[Export("floatFormatWithNumberOfChannels:sampleRate:")]
		AudioStreamBasicDescription FloatFormatWithNumberOfChannels(uint channels, float sampleRate);

		// +(AudioStreamBasicDescription)M4AFormatWithNumberOfChannels:(UInt32)channels sampleRate:(float)sampleRate __attribute__((deprecated("")));
		[Static]
		[Export("M4AFormatWithNumberOfChannels:sampleRate:")]
		AudioStreamBasicDescription M4AFormatWithNumberOfChannels(uint channels, float sampleRate);

		// +(AudioStreamBasicDescription)monoFloatFormatWithSampleRate:(float)sampleRate __attribute__((deprecated("")));
		[Static]
		[Export("monoFloatFormatWithSampleRate:")]
		AudioStreamBasicDescription MonoFloatFormatWithSampleRate(float sampleRate);

		// +(AudioStreamBasicDescription)monoCanonicalFormatWithSampleRate:(float)sampleRate __attribute__((deprecated("")));
		[Static]
		[Export("monoCanonicalFormatWithSampleRate:")]
		AudioStreamBasicDescription MonoCanonicalFormatWithSampleRate(float sampleRate);

		// +(AudioStreamBasicDescription)stereoCanonicalNonInterleavedFormatWithSampleRate:(float)sampleRate __attribute__((deprecated("")));
		[Static]
		[Export("stereoCanonicalNonInterleavedFormatWithSampleRate:")]
		AudioStreamBasicDescription StereoCanonicalNonInterleavedFormatWithSampleRate(float sampleRate);

		// +(AudioStreamBasicDescription)stereoFloatInterleavedFormatWithSampleRate:(float)sampleRate __attribute__((deprecated("")));
		[Static]
		[Export("stereoFloatInterleavedFormatWithSampleRate:")]
		AudioStreamBasicDescription StereoFloatInterleavedFormatWithSampleRate(float sampleRate);

		// +(AudioStreamBasicDescription)stereoFloatNonInterleavedFormatWithSampleRate:(float)sampleRate __attribute__((deprecated("")));
		[Static]
		[Export("stereoFloatNonInterleavedFormatWithSampleRate:")]
		AudioStreamBasicDescription StereoFloatNonInterleavedFormatWithSampleRate(float sampleRate);

		// +(BOOL)isFloatFormat:(AudioStreamBasicDescription)asbd __attribute__((deprecated("")));
		[Static]
		[Export("isFloatFormat:")]
		bool IsFloatFormat(AudioStreamBasicDescription asbd);

		// +(BOOL)isInterleaved:(AudioStreamBasicDescription)asbd __attribute__((deprecated("")));
		[Static]
		[Export("isInterleaved:")]
		bool IsInterleaved(AudioStreamBasicDescription asbd);

		// +(BOOL)isLinearPCM:(AudioStreamBasicDescription)asbd __attribute__((deprecated("")));
		[Static]
		[Export("isLinearPCM:")]
		bool IsLinearPCM(AudioStreamBasicDescription asbd);

		// +(void)printASBD:(AudioStreamBasicDescription)asbd __attribute__((deprecated("")));
		[Static]
		[Export("printASBD:")]
		void PrintASBD(AudioStreamBasicDescription asbd);

		// +(NSString *)displayTimeStringFromSeconds:(NSTimeInterval)seconds __attribute__((deprecated("")));
		[Static]
		[Export("displayTimeStringFromSeconds:")]
		string DisplayTimeStringFromSeconds(double seconds);

		// +(NSString *)stringForAudioStreamBasicDescription:(AudioStreamBasicDescription)asbd __attribute__((deprecated("")));
		[Static]
		[Export("stringForAudioStreamBasicDescription:")]
		string StringForAudioStreamBasicDescription(AudioStreamBasicDescription asbd);

        //todo write wrapper: had AudioStreamBasicDescription*
		// +(void)setCanonicalAudioStreamBasicDescription:(AudioStreamBasicDescription *)asbd numberOfChannels:(UInt32)nChannels interleaved:(BOOL)interleaved __attribute__((deprecated("")));
		[Static]
		[Export("setCanonicalAudioStreamBasicDescription:numberOfChannels:interleaved:")]
        void SetCanonicalAudioStreamBasicDescription(IntPtr asbd, uint nChannels, bool interleaved);

        //todo write wrapper: 2 float*
		// +(void)appendBufferAndShift:(float *)buffer withBufferSize:(int)bufferLength toScrollHistory:(float *)scrollHistory withScrollHistorySize:(int)scrollHistoryLength __attribute__((deprecated("")));
		[Static]
		[Export("appendBufferAndShift:withBufferSize:toScrollHistory:withScrollHistorySize:")]
        void AppendBufferAndShift(IntPtr buffer, int bufferLength, IntPtr scrollHistory, int scrollHistoryLength);

        //todo write wrapper: had float*
		// +(void)appendValue:(float)value toScrollHistory:(float *)scrollHistory withScrollHistorySize:(int)scrollHistoryLength __attribute__((deprecated("")));
		[Static]
		[Export("appendValue:toScrollHistory:withScrollHistorySize:")]
        void AppendValue(float value, IntPtr scrollHistory, int scrollHistoryLength);

		// +(float)MAP:(float)value leftMin:(float)leftMin leftMax:(float)leftMax rightMin:(float)rightMin rightMax:(float)rightMax __attribute__((deprecated("")));
		[Static]
		[Export("MAP:leftMin:leftMax:rightMin:rightMax:")]
		float MAP(float value, float leftMin, float leftMax, float rightMin, float rightMax);

        //todo write wrapper: had float*
		// +(float)RMS:(float *)buffer length:(int)bufferSize __attribute__((deprecated("")));
		[Static]
		[Export("RMS:length:")]
        float RMS(IntPtr buffer, int bufferSize);

		// +(float)SGN:(float)value __attribute__((deprecated("")));
		[Static]
		[Export("SGN:")]
		float SGN(float value);

        //todo write wrapper: had sbyte*
		// +(void)checkResult:(OSStatus)result operation:(const char *)operation __attribute__((deprecated("")));
		[Static]
		[Export("checkResult:operation:")]
        void CheckResult(int result, IntPtr operation);

		// +(NSString *)stringFromUInt32Code:(UInt32)code __attribute__((deprecated("")));
		[Static]
		[Export("stringFromUInt32Code:")]
		string StringFromUInt32Code(uint code);

        //todo write wrapper: float** int*, float*, bool*
		// +(void)updateScrollHistory:(float **)scrollHistory withLength:(int)scrollHistoryLength atIndex:(int *)index withBuffer:(float *)buffer withBufferSize:(int)bufferSize isResolutionChanging:(BOOL *)isChanging __attribute__((deprecated("")));
		[Static]
		[Export("updateScrollHistory:withLength:atIndex:withBuffer:withBufferSize:isResolutionChanging:")]
        void UpdateScrollHistory(IntPtr scrollHistory, int scrollHistoryLength, IntPtr index, IntPtr buffer, int bufferSize, IntPtr isChanging);

        //todo write wrapper: had TPCircularBuffer*
		// +(void)appendDataToCircularBuffer:(TPCircularBuffer *)circularBuffer fromAudioBufferList:(AudioBufferList *)audioBufferList __attribute__((deprecated("")));
		[Static]
		[Export("appendDataToCircularBuffer:fromAudioBufferList:")]
        void AppendDataToCircularBuffer(IntPtr circularBuffer, AudioBuffers audioBufferList);

		//todo write wrapper: had TPCircularBuffer*
		// +(void)circularBuffer:(TPCircularBuffer *)circularBuffer withSize:(int)size __attribute__((deprecated("")));
		[Static]
		[Export("circularBuffer:withSize:")]
        void CircularBuffer(IntPtr circularBuffer, int size);

		//todo write wrapper: had TPCircularBuffer*
		// +(void)freeCircularBuffer:(TPCircularBuffer *)circularBuffer __attribute__((deprecated("")));
		[Static]
		[Export("freeCircularBuffer:")]
        void FreeCircularBuffer(IntPtr circularBuffer);
	}
}