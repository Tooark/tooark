using Google.Apis.Auth.OAuth2;

namespace Tooark.Tests.Moq.Dto;

public class GcpCredentialDto
{
  public JsonCredentialParameters GCP { get; set; } = new JsonCredentialParameters()
  {
    Type = "service_account",
    ProjectId = "test-project",
    PrivateKeyId = "9e15a00659f57d9aebba8abf8183a9ebade88e1",
    PrivateKey = "-----BEGIN PRIVATE KEY-----\nMIIEvwIBADANBgkqhkiG9w0BAQEFAASCBKkwggSlAgEAAoIBAQCcnci2PaAsyCQq\n9oeH2ug6saQbTJ8TPrexdavdICwgeI947uqfWUmrTdleoM9C/VmWvPLo8TyEbLU7\niF907WcHPWscvPaQmAgga+LWq92oCswXzPTnc3VcxlZpJz4FtLrjzgt9pzgo1RlB\ngm6T2rEYUI/DgXlqr5jSAisEBXDuAf/nS9ZJ8HwcQToL+MmwiRwWAouTBB0zRAyw\nS4W972pVeBlXfG+JJs9ji8cmTCYp/gzgSsgDfiUwvpUq8kHSjFga5Rxxsjse4s/c\nthmjnTHaekiQLtRRMtexF4f0ic1Yatup/4I/j4hcSqsrpRLs1n9HYUH9RIxifIXN\nRwyyLbFdAgMBAAECggEAJM6vRmc+zu99koxMZ2kS5YJsxU6xXxJBFSeIy+f8/uee\nnmcWBHu1RNl/sc3c0AtfQ/4ln4ncdWngAP0IpCAMM+cjFoaxcuOMG2SHk/ih2BWr\niBdu4jtgFHz3f3CN0sT6HEJHMJF9cRk9/YekKcL+lm6Ojv3NlX1tzsNcLS6DLygy\nhw8tPiFtpYb90zYkV4E/cZPypSeS2C+QOcch6hFIY/HnVW+znkBxHcsRTGeDV6JK\nwtQ4FY0QFFKoUwYcwwenRHgkgmp53TI3ERO/l/xA5xAdjB828RmN0ESEfWXXBYRH\nLZ5hAfoDzHpRCet7QUAGhL3cP64xGXL94X5MORhasQKBgQDKQ6ktsRl+bJbWWu1C\newv7NFwNQYCVap/NHO2Y48+1m2djTbE+v2SM8xjKfAA7pS/iYrBu68wJyTXHd5uj\nA0dgxCBO4Gp/0c4kgsZ3ErdgjAf34764/wMzlnVWPkfY0Qa61oXKTN3H5cvizrhA\nuP0NEevULW04f5dTF6FBYW7vEQKBgQDGOYcruzA1JZn0KWGZToS4jf2ZoQuo/xS1\nB7yWLLS2pZu6bLiUD8k51HEfuCHpLafCyD94Y7R9b8qDEcim5LQZ8iST0Sm+KDOi\nSs023//V3RZULTmieSQ6YILGvJ0jmP3sOxXZd3D6w99FakJHaezWm30If4rKuDch\nNTxBgj21jQKBgQDAfK2nFzB82RifPH8o0nLviL/Fub2F9KfJiqsu4jaMzS0SGyD/\n5yLo5HyGToXmPWkZl/PyS6Ks+XidsF1EQF2QYHWiuv1/UQpOQspfDUfIsNrwdSPP\naJkCYkCeZ7z2QM6XxooioQ47K5zh5vsAVUw1QQXesbCMoRhA8m77Cp+JEQKBgQCh\njqWSiOADkP/nNgrrzkjxuhhlLBK6zBtuaD3WD93BtzwzNwVA95TD41fHyGUrDSDb\n6iWA05J5YbGmHcpx65i2RNp291SUPWPH9DtJbEuxXJ4kQS/mMeHLCnnLzFIufzUF\nYiqusWCNoTLJ+o7GcZisWNIRKRJPotCb6i2QHrx4yQKBgQCf9+TswuUaNV0MIBAd\nkUkHLJzZhuam8VAKWs6SIHNvN9nikhITNRAtnQyG9nOerrg/5bo2ec/hGYzXLUmB\nmUjC+j6taw3dlkVf9g1AOU/5tppM1eynjHKzeWzPRTTno4wm5mseqq9u2Kgn1HJq\n1vnwey6Em6Tfm5tlGqkMvcJ5Kw==\n-----END PRIVATE KEY-----\n",
    ClientEmail = "fakeuser@test-project.iam.gserviceaccount.com",
    ClientId = "118423318392668648094",
    TokenUri = "https://oauth2.googleapis.com/token",
    ClientSecret = "https://www.googleapis.com/robot/v1/metadata/x509/fakeuser%test-project.iam.gserviceaccount.com",
    UniverseDomain = "googleapis.com"
  };
}
