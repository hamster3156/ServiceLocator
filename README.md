# ダウンロード方法
`UPMリンク`\
https://github.com/hamster3156/ServiceLocator.git?path=Library/ServiceLocator

`unitypackage`\
[https://github.com/hamster3156/ServiceLocator/releases/tag/unitypackage](https://github.com/hamster3156/ServiceLocator/releases/tag/v1.0.1)

# 利用方法
サービスロケーターはC#のstaticクラスで作成されているので、MonoBehaviour、C#どちらでも利用できます。\
利用手順として、インスタンスを登録→ロケーターからインスタンスを取得→ゲーム実行終了時に破棄する\
ソースコードの場合、以下の内容になります。
```C#
 /*
  * インスタンス登録処理
  * どちらも同じインスタンスを登録すると上書きされます
  * ロケーターにインスタンスを登録するために、Registerメソッドを使用します
  * 上が単一インスタンスを登録する方法で、下は都度生成インスタンスを登録する方法です
  */
 ServiceLocator.Register<ITest>(this);
 ServiceLocator.Register<ITest, TestManager>();

 /*
  * インスタンス取得処理
  * インスタンスを取得するために、Resolveメソッドを使用します
  * サービスロケーターに登録するインスタンスはインターフェースを利用する事をオススメします。
  * 単一、都度に同じインスタンスを登録した場合、単一インスタンスが優先されて取得されます。
  */
 var singleTestKun = ServiceLocator.Resolve<ITest>();
 var factoryTestKun = ServiceLocator.Resolve<ITest>();

 /*
  * インスタンスの登録を確認する処理
  * インスタンスが登録されているかを確認するために、IsRegisteredメソッドを利用して確認します
  * 単一、都度生成のどちらかにインスタンスが登録されている場合はtrueを返します。
  * どちらも登録されていない場合はfalseを返します。
  */
 if (ServiceLocator.IsRegistered<ITest>())
 {
     singleTestKun.Test();
     factoryTestKun.Test();
 }

 /*
  * 登録インスタンスの解除処理
  * staticでインスタンスを管理しているので、終了時に登録したインスタンスを解除する必要があります。
  * 登録したインスタンスを解除するために、Unregisterメソッドをゲームの実行終了時に使用してください。
  * 単一、都度生成に登録されているインスタンスをどちらも解除します
  */
 ServiceLocator.Unregister<ITest>();
```

# アウトプットで書いた記事\
https://qiita.com/game_hamster/items/9dd5c84c2de601e85224
