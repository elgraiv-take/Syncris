# Syncris
ファイル同期ツール

## 準備
* ビルドする
* Syncris.exe.configのDefaultTargetRootPathをコピー先のルートに書き換える
* 前提としてコピー先のどこかにコピー元と同名のファイルがあるようにする
## 使い方
* コピー元のファイルをテーブルにドラッグ&ドロップで放り込む
* Auto Assignを押すと準備で設定したコピー先がアサインされるはず
* Start/Stopで同期開始停止
* Save/LoadでリストをJSONに読み書き
  * コピー先のルートパスもこのファイルに書かれる

# TODO
* パスの編集/削除（今は保存したJSONを直接編集するしかない）
* タスクトレイ常駐化
* 自動アサイン用のパスの編集（今は保存したJSONを直接編集するしかない）

# その他
## 依存ライブラリ
https://www.newtonsoft.com/json
