# GoogleAdmob-Unity
Google Admob for Unity

整理Google Admob 方便Unity项目使用。

## 使用方式

1. iOS 必须先安装 cocoapods，建议使用 `Homebrew` 进行安装。
    * 安装 `Homebrew` 
        1.  `/usr/bin/ruby -e "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/master/install)"`
        1. 复制上面的命令在 `Terminal（终端）` 中执行
    * 安装 `cocoapods` : ` brew install cocoapods`
    
1. 使用命名空间
    ```c#
    using GoogleMobileAds.Custom;
    using GoogleMobileAds.Api;
    ```

1. 广告分类
    * AdmobType.BannerView 横幅广告
    * AdmobType.Interstitial 插屏广告
    * AdmobType.Reward  激励视频

### 初始化

1. 横幅广告
    *  初始化横幅广告 `CustomAdManager.Instance.InitAdmob(AdSize.Banner, AdPosition.Bottom);`
    *   `AdSize`  控制横幅的大小，默认是 `Banner`  可以自行尝试大小，暂不支持指定位置
    *   `AdPosition` 控制横幅广告显示的位置
    
1. 插屏广告
    * 初始化插屏广告 `CustomAdManager.Instance.InitAdmob(AdmobType.Interstitial, onOpened, onClosed, onAdLeaveApplication);`
    * 后面的三个回调函数可以直接传 null 值，建议在 `onOpened` 回调中将游戏本身的声音关闭，在另外两个回调中将声音打开。 
    
1. 激励视频广告
    * `CustomAdManager.Instance.InitAdmob(AdmobType.RewardAd, onOpened, onClosed, onAdLeaveApplication);`
    * 后面的三个回调函数可以直接传 null 值，建议在 `onOpened` 回调中将游戏本身的声音关闭，在另外两个回调中将声音打开。
    * 激励视频是用户看了广告之后奖励指定物品给玩家，可以用来替代内购
    
### 展示

1. 通用调用方式 : `CustomManager.Instance.ShowAd(AdmobType.xxxx);`

1. 激励视频广告一定要在 `ShowAd` 方法调用时传入 `RewardCallback` , 函数需要两个接受回调的参数 `(double amount,string type){}`

1. 除了横幅广告，其他的广告类型调用 `CloseAd(AmobType)` 方法无效

### 注意

1. iOS 打包为 `XCode` 项目之后需要剔除一个 `libPods-Unity-iPhone.a` 库文件才能正常打包。
    

    

