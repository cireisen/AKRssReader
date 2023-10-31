Programmed with VisualStudio2022 C# 6.0

# AKRssReader
AKRssReader는 명일방주 비리비리 공식계정 최신 글을 RSS로 읽어 디스코드 웹훅으로 보내는 소스입니다.

AKRssReader read announce from from Arknights official bilibili account. and send to discord webhook.

AKRssReaderは自動的にアークナイツのビリビリ公式アカウントの最新お知らせをRSSで読んでdiscordのwebhookで送るコードです。

# HowToUse
공지 내용을 보내고싶은 서버의 설정-연동-웹훅에서 공지를 보낼 웹훅의 주소를 복사합니다.

bin에 resouce폴더를 만들고, Config.json 파일을 작성합니다.

```
{
  "WebHookLink": "",
  "ThreadID":""
}
```

각각 이름에 맞추어 내용을 기입하고, 

ThreadID에는 채널 내 특정 스레드에 보내고 싶으면 스레드 ID를 복사하여 기입합니다.

프로젝트를 빌드하여 확인합니다.
