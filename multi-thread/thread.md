 Thread.Sleep(0)
 Thread.Yield() 

### 阻塞
	- 如果线程的执行由于某种原因导致暂停，那么就认为该线程被阻塞了。eg，Sleep或Join
	- 被阻塞的线程会立即将其处理器上的时间片生成给其他线程，从此不再消耗处理器时间，直到满足其阻塞条件为止
	- 通过ThreadState属性判断阻塞状态
		bool blocked = (thread.ThreadState & ThreadState.WaitSleepJoin) != 0
### 解除阻塞UnBlocking
	1. 阻塞条件被满足
	2. 操作超时
	3. 通过Thread.Interrupt()进行打断
	4. 通过Thread.Abort()进行中止
### 上下文切换
	当线程阻塞或解除阻塞时，操作系统将执行上下文切换。这会产生少量开销，通常为1或2微妙
### I/O-bound vs Compute-bound（或 CPU-bound）
	- 一个花费大部分时间等待某事发生的操作称为 I/O-bound
    - 一个花费大部分时间执行CPU密集型工作的操作称为 Compute-bound
### 阻塞 vs 忙等待（自旋） Blocking vs Spinning
	- I/O-bound 操作的工作方式有两种
		1. 在当前线程上同步的等待 Console.ReadLine(),Thread.Sleep().Thread.Join()...
		2. 异步的操作，在稍后操作完成时触发一个回调动作
	- 同步等待的 I/O-bound 操作将大部分时间花在阻塞线程上
	- 它们也可以周期性的在一个循环里进行“打转（自旋）”
		```
			while (DateTime.Now < nextStartTime)
				Thread.Sleep();

			while (DateTime.Now < nextStartTime)
		```
	- 在忙等待和阻塞之间有一些细微的差别
		首先，如果您希望条见很快得到满足（可能在几微秒之内），则短暂自旋可能会很有效，因为它避免了
		上下文切换的开销和延迟
		其次，阻塞也不是零成本。这是因为每个线程在生存期间会占用大约1MB的内存，并会给CLR和
		操作系统带来持续的管理开销
		因此，在需要处理成百上千个并发操作的大量I/O-bound程序的上下文中，阻塞可能会很麻烦
		所以，此类程序需要使用基于回调的方法，在等待时完全撤销其线程

### 本地 vs 共享的状态 Local vs Shared State
	- Local
		CLR为每个线程分配自己的内存栈（Stack），以便使本地变量保持独立
	- Shared 
		如果多个线程都引用到同一个对象的实例，那么它们就共享了数据
		被Lambda表达式或匿名委托所捕获的本地变量，会被编译器转化为字段（field），所以也会被共享
		静态字段（field）也会在线程间共享数据
### 线程安全
	- 在读取和写入数据的时候，通过使用一个互斥锁（exclusive lock）
	- C#使用lock语句来加锁
	- 当两个线程同时竞争一个锁的时候（锁可以基于任何引用类型对象），一个线程会等待或阻塞，直到锁
	  变成可用状态
	- Lock不是线程安全的银弹，很容易忘记对字段加锁，lock也会引起一些问题（死锁）
### 向线程传递数据
	- 使用lambda表达式
	- 在C#3.0之前，没有lambda表达式。可以使用Thread的Start方法来传递参数
### Lambda表达式与被捕获的变量
	- 使用Lambda表达式可以很简单的给Thread传递参数。但是线程开始后，可能会不小心修改了被捕获的变量
### 异常处理
	- 创建线程时在作用范围内的try/catch/finally块，在线程开始执行后就与线程无关了
	- 在WPF、WinForm里，可以订阅全局异常处理事件：
		Application.DispatcherUnHandledException
		Application.ThreadException
		在通过消息循环调用的程序的任何部分发生未处理的异常（这相当于应用程序处于活动状态时在主线程上
		运行的所有代码）后，将触发这些异常
		但是非UI线程上的未处理异常，并不会触发它
	- 而任何线程有任何未处理的异常都会触发
		AppDomain.CurrentDomain.UnhandledException
### 前台和后台线程 Foreground vs Background Threads
	- 默认情况下，你手动创建的线程就是前台线程
	- 只要前台线程在运行，那么应用程序就会一直处于活动状态
		但是后台线程却不行
		一旦所以前台线程停止，那么应用程序就停止了
		任何的后台线程也会突然终止
		注意：线程的前台、后台状态与它的优先级无关（所分配的执行时间）
	- 可以通过IsBackground属性判断线程是否是后台线程
	- 进程以这种形式终止的时候，后台线程执行栈中的finally块就不会执行了
		如果想让它执行，可以在退出程序时使用Join来等待后台线程（如果是你自己创建的线程），或使用
		signal construct，如果是线程池...
	- 应用程序无法正常退出的一个常见原因时是还有活跃的前台线程

### 线程的优先级
	- 线程的优先级（Thread的Priority属性）决定了相对于操作系统中其它活跃线程所占的执行时间
	- 优先级分为：
		enum ThreadPriority {Lowest,BelowNormal,Normal,AboveNormal,Highest}
### 提升线程优先级
	- 提升线程优先级的时候需要特别注意，因为它可能“饿死”其它线程
	- 如果想让某线程（Thread）的优先级比其他进程（Process）中的线程（Thread）高，那就必须提升
	  进程（Process）的优先级
		使用Synstem.Diagnostics下的Process类
			```
				using (Process p = Process.GetCurrentProcess())
					p.PriorityClass = ProcessPriorityClass.High;
			```
	- 这可以很好地用于只做少量工作且需要较低延迟的非UI进程
	- 对于需要大量计算的应用程序（尤其是有UI的应用程序），提高进程优先级可能会使其它进程饿死，
	  从而降低整个计算机的速度

### 信号 Signaling
	- 有时，你需要让某线程一直处于等待的状态，直到接收到其它线程发来的通知。这就叫做信号Signaling
	- 最简单的信号结构就是ManualResetEvent
		调用它上面的WaitOne方法会阻塞当前的线程，直到另一个线程通过调用Set方法来开启信号

### 富客户端应用程序的线程
	- 在WPF、UWP、WinForm等类型的程序中，如果在主线程执行耗时的操作，就会导致整个程序无响应。因为
	  主线程同时还需要处理消息循环，而渲染和鼠标键盘事件处理等工作都是消息循环来执行的
	- 针对这种耗时的操作，一种流行的做法是启用一个worker线程。执行完操作和，再更新到UI
	- 富客户端应用的线程模型通常是：
		UI元素和控件只能从创建它们的线程来进行访问（通常是主UI线程）
		当想从worker线程更新到UI的时候，你必须把请求交给UI线程
	- 比较底层的实现是：
		WPF，在元素的Dispatcher对象上调用BeginInvoke或Invoke
		WinForm，调用控件的BeginInvoke或Invoke
		UWP，调用Dispatcher对象上的RunAsync或Invoke
	- 所有这些方法都接收一个委托
	- BeginInvoke或RunAsync通过将委托排队到UI线程的消息队列来执行工作
	- Invoke执行相同的操作，但随后会进行阻塞，直到UI线程读取并处理消息
	  因此，Invoke允许您从方法中获取返回值

### 同步上下文 Synchronization Contexts
	- 在System.ComponentModel下有一个抽象类：SynchronizationContext，它使得Thread Marshaling
	  得到泛化
	- 针对移动、桌面（WPF，UWP，WinForm）等富客户端应用的API，它们都定义和实例化了
	  SynchronizationContext的子类
		可以通过静态属性SynchronizationContext.Current来获得（当运行在UI线程时）
		捕获该属性让你可以在稍后的时候从worker线程向UI线程发送数据
		调用Post相当于调用Dispatch或Control上的BeginInvoke方法
		还有一个Send方法，它等价于Invoke方法 

### 线程池 Thread Pool
	- 当开始一个线程的时候，将花费几百微秒来组织类似以下的内容：
		一个新的局部变量栈（Stack）
	- 线程池就可以节省这种开销：
		通过预先创建一个可循环使用线程的池来减少这一开销
	- 线程池对于高效的并行编程和细粒度并发是必不可少的
	- 它允许在不被线程启动的开销淹没的情况下运行短期操作
### 使用线程池线程需要注意以下几点
	- 不可以设置池线程的Name
	- 池线程都是后台线程
	- 阻塞池线程可使性能降级
	- 你可以自由的更改池线程的优先级
		当它放回池的时候优先级将还原为正常状态
	- 可以通过Thread.CurrentThread.IsThreadPoolThread属性来判断是否执行在池线程上
### 进入线程池
	- 最简单的、显式的在池线程运行代码的方式就是使用Task.Run
### 谁使用了线程池
	- WCF、Remoting、ASP.NET、ASMX Web Services应用服务器
	- System.Timers.Timer、System.Threading.Timer
	- 并行编程结构
	- BackgroundWorker类（现在很多余）
	- 异步委托（现在很多余）
### 线程池中的整洁
	- 线程池提供了另一个功能，即确保临时超出计算-Bound的工作不会导致CPU超额订阅
	- CPU超额订阅：活跃的线程超过CPU的核数，操作系统就需要对线程进行时间切片
	- 超额订阅对性能影响很大，时间切片需要昂贵的上下文切换，并且可能使CPU缓存失效，
      而CPU缓存对于现代处理器的性能至关重要
#### CLR的策略
	- CLR通过对任务排队并对其启动进行节流限制来避免线程池中的超额订阅
	- 它首先运行尽可能多的并发任务（只要还有CPU核），然后通过爬山算法调整并发级别，
      并在特定方向上不断调整工作负载
		如果吞吐量提高，它将继续朝同一方向（否则将反转）
	- 这确保它始终追随最佳性能曲线，即使面对计算机上竞争的进程活动时也是如此
	- 如果下面两点能够满足，那么CLR的策略将发挥出最佳效果：
		工作项大多是短时间运行的（<250毫秒，或在理想情况<100毫秒），因此CLR有很多
		机会进行测量和调整
		大部分时间都被阻塞的工作项不会主宰线程池

### Thread的问题
	- 线程（Thread）是用来创建并发（concurrency）的一种低级别工具，它有一些限制，尤其是：
		虽然开始线程的时候可以方便的传入数据，但是当Join的时候，很难从线程获得返回值
			可能需要设置一些共享字段
			如果操作抛出异常，捕获和传播该异常都很麻烦
		无法告诉线程在结束时开始做另外的工作，你必须进行Join操作（在进程中阻塞当前的线程）
	- 很难使用较小的并发（concurrentt）来组件大型的并发
	- 导致了对手动同步的更大依赖以及随之而来的问题

	-Task类可以很好的解决上述问题
### Task class
	- Task是一个相对高级的对象：它代表了一个并发操作（concurrent）
		该操作可能由Thread支持，或不由Thread支持
	- Task是可组合的（Continuation）
		Task可以使用线程池来减少启动延迟
		使用TaskCompletionSource，Task可以利用回调的方式，在等待I/O绑定操作时完全避免线程
### 开始一个Task
	- Task类在System.Threading.Tasks命名空间下
	- Task.Run(委托)
	- Task默认使用线程池，也就是后台线程
		当主线程结束时，你创建的所有tasks都会结束
