using BenchmarkDotNet.Attributes;
using Tooark.Utils;

namespace Tooark.Benchmarks.Utils;

// https://benchmarkdotnet.org/articles/overview.html

[MemoryDiagnoser]
public class NormalizedBenchmark
{
  static readonly List<string> _strings = new()
  {
    "Olá, eu sou o Tooark!",
    "¿Qué tal? Me llamo Tooark.",
    "Wie geht's? Ich heiße Tooark.",
    "こんにちは、私はTooarkです。",
    "你好，我叫Tooark。",
    "안녕하세요, 저는 Tooark입니다.",
    "Привет, я Tooark.",
    "Hej, jag heter Tooark.",
    "Hoi, ik heet Tooark.",
    "Cześć, nazywam się Tooark.",
    "Hei, jeg heter Tooark.",
    "Hei, nimeni on Tooark.",
    "Ahoj, jmenuji se Tooark.",
    "Szia, Tooark vagyok.",
    "Salut, je suis Tooark.",
    "Hola, soy Tooark.",
    "Hallo, ich bin Tooark.",
    "Hello, I'm Tooark.",
    "Ciao, sono Tooark.",
    "Γεια σας, είμαι ο Tooark.",
    "Merhaba, ben Tooark.",
    "Hej, jeg hedder Tooark.",
    "Bună, eu sunt Tooark.",
    "Zdravo, ja sam Tooark.",
    "Sveiki, aš esu Tooark.",
    "Здраво, јас сум Tooark.",
    "Здравейте, аз съм Tooark.",
    "Pozdravljeni, jaz sem Tooark.",
    "Sawubona, ngiyi-Tooark.",
    "Tere, mina olen Tooark.",
    "Labas, aš Tooark.",
    "Sveiki, es esmu Tooark.",
    "Salve, ego sum Tooark.",
    "Bok, ja sam Tooark.",
    "Dia duit, is mise Tooark.",
    "Shwmae, fi yw Tooark.",
    "Halo, saya Tooark.",
    "Kamusta, ako si Tooark.",
    "สวัสดีครับ ผมชื่อ Tooark",
    "नमस्ते, मेरा नाम Tooark है।",
    "नमस्कार, माझे नाव Tooark आहे.",
    "નમસ્તે, મારું નામ Tooark છે.",
    "ਸਤਿ ਸ੍ਰੀ ਅਕਾਲ, ਮੇਰਾ ਨਾਮ Tooark ਹੈ.",
    "வணக்கம், என் பெயர் Tooark.",
    "నమస్కారం, నా పేరు Tooark.",
    "ನಮಸ್ಕಾರ, ನನ್ನ ಹೆಸರು Tooark.",
    "നമസ്കാരം, എന്റെ പേര് Tooark.",
    "হ্যালো, আমার নাম Tooark.",
    "sdùVWMäwúý",
    "ûëlbsSô1óv",
    "33dòïêgDQR",
    "0óaïJnYëMí",
    "Nð 1379Qli",
    "ÿm0oeTEmbã",
    "äCjqåqjVZq",
    "Tf4îJåzàge",
    "ù1nCÿ0ú7éW",
    "òî7gåP0siH",
    "Tõú7rIhnlD",
    "QäWewíïd0r",
    "IéíP1ìelòë",
    "ô6cYcPêáoE",
    "XBkáaUsäÿô",
    "ôöswv8ühñô",
    "SB8làbhûH1",
    "âdçd51KU8 ",
    "cMF28vå8FS",
    "êâCeýJ1ugô",
    "àNng6Ip aù",
    "ôLEdvôqbîj",
    "tð57Q4lnLR",
    "bçzhlzðíRö",
    "oOLáêðìQúR",
    "îarkvEHeTã",
    "ZzHtdóflj ",
    "HyyEtoL8Sm",
    "àýoçflPyei",
    "O7ãurè5ùUü",
    "ðHzEH5kBHt",
    "âpSeâIôðnR",
    "â6IÿfiDô6b",
    "H4ðçwYTv5ê",
    "LýòyHjhëvx",
    "lÿâSwx5ReX",
    "òX6áO7y Xm",
    "xWA4pQèféõ",
    "S7L61Fõô1O",
    "ôkxVÿn6yKj",
    "FáSãïavmñH",
    "îPkzk2Zjgå",
    " jêñyàýtïV",
    "1jicíqëvå6",
    "wWò1yAy5r2",
    "àY70Fäamàð",
    "enîlêçv6Gü",
    "ýðohfWMùhz",
    "ãaócã9iuTé",
    "CjcdGcSVDê",
    "kJñzVn3âçk",
    " 7O0OTBYM ",
    "Dgèzlýðiü3",
    "6öäädñéHPG",
    "6Bï3ðfóîeX",
    "ádGTgYmãUç",
    "PõîF3B7aNW",
    "jzhêXBìBwñ",
    "ùjh0räàíö4",
    "ÿf2òiãkOOð",
    "19P5ébxèCV",
    "öróîìâVõdÿ",
    "6h0ìFëúP5u",
    "í1KäéaëaäK",
    "SçfpRã2ðAX",
    "S7dsrô72Zú",
    "Ycn8xZUF7Q",
    "oéöìoyKgsd",
    "lílM5LBîTO",
    "OdM28XwhWe",
    "QPMc3OA7hN",
    "RR5ër7LñrS",
    "HFVJóJPf8c",
    "pUpCBãTöUô",
    "ôùH4Ilpnç3",
    "yMðWg7Múöá",
    "7x7låBIôýe",
    "yeZaRàïüN ",
    "QBzQçov åó",
    "íu8ÿnFCCu8",
    "hN7õëwüü I",
    "EfVWùlmAëa",
    "R7ïRwúõbäN",
    "K0êbnRDìYY",
    "rdKUývûuvl"
  };

  [Benchmark(Baseline = true)]
  public void NormalizeValue()
  {
    // Normaliza cada string da lista usando o método NormalizeValue
    foreach (string s in _strings)
    {
      _ = Util.NormalizeValue(s);
    }
  }

  [Benchmark]
  public void NormalizeValueRegex()
  {
    // Normaliza cada string da lista usando o método NormalizeValueFor
    foreach (string s in _strings)
    {
      _ = Util.NormalizeValueRegex(s);
    }
  }
}
