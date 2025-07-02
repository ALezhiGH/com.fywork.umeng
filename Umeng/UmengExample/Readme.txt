请看如下说明,能解决大部分的集成问题

        1.如何集成
        第一步:解压zip后找到UmengGameAnalytics_v3.x.unitypackage,导入这个包.(无需导入Common.unitypackage)
        第二步:把GA.StartWithAppKeyAndChannelId("59892f08310c9307b60023d0", "App Store")中的59892f08310c9307b60023d0改成你的appkey
        
        注意:
        不要改动Analytics.cs DplusAgent.cs GA.cs SimpleJSON.cs UmengAndroidLifeCycleCallBack.cs的代码
        不要把UmengAndroidLifeCycleCallBack.cs挂在GameObject上



        2.如何通过Log判断发送数据时候成功


            0.确保手机能连上网络
            1.把手机连上电脑
            2.打开xcode或者android studio,
            3.启动app并触发事件
            4.等待40秒
            5.按Home键将app退出后台
            6.重新进入app 观察xcode log视图 或者 android studio logcat视图
            7.android平台日志过滤方法为 tag:MobclickAgent
             ios平台日志 为:MobClick开头的log


        3.后台看到数据多久能看到

           后台数据会延迟一段时间才能看到.通过上述2的方法确保数据发送成功后,请等待一段时间(根据后台负债情况 延迟在1到30分钟)再去后台查看数据
