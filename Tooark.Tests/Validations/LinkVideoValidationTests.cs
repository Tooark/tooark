using Tooark.Validations;

namespace Tooark.Tests.Validations;

public class LinkVideoValidationTests
{
  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("https://www.y0utube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://vime0.com/24812648")]
  [InlineData("https://www.dailym0tion.com/video/x9fbqre")]
  [InlineData("")]
  [InlineData(null)]
  public void IsLinkVideo_ShouldAddNotification_WhenValueNotIsLinkVideo(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsLinkVideo(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("https://www.youtube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://youtu.be/b6-JNeXxN3s?si=Q3mm2FArjTqUf7I6")]
  [InlineData("https://youtube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://www.youtube.com/embed/b6-JNeXxN3s?si=9ON8XlL6BaqygVkW")]
  [InlineData("https://vimeo.com/24812648")]
  [InlineData("https://vimeo.com/24812648?share=copy")]
  [InlineData("https://player.vimeo.com/video/24812648?badge=0&autopause=0&player_id=0&app_id=58479")]
  [InlineData("https://www.dailymotion.com/video/x9fbqre")]
  [InlineData("https://dai.ly/x9fbqre")]
  [InlineData("https://geo.dailymotion.com/player.html?video=x9fbqre")]
  public void IsLinkVideo_ShouldNotAddNotification_WhenValueIsLinkVideo(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsLinkVideo(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("https://vimeo.com/24812648")]
  [InlineData("https://vimeo.com/24812648?share=copy")]
  [InlineData("https://player.vimeo.com/video/24812648?badge=0&autopause=0&player_id=0&app_id=58479")]
  [InlineData("https://www.dailymotion.com/video/x9fbqre")]
  [InlineData("https://dai.ly/x9fbqre")]
  [InlineData("https://geo.dailymotion.com/player.html?video=x9fbqre")]
  [InlineData("https://www.y0utube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://vime0.com/24812648")]
  [InlineData("https://www.dailym0tion.com/video/x9fbqre")]
  [InlineData("")]
  [InlineData(null)]
  public void IsLinkVideoYouTube_ShouldAddNotification_WhenValueNotIsLinkVideoYouTube(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsLinkVideoYouTube(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("https://www.youtube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://youtu.be/b6-JNeXxN3s?si=Q3mm2FArjTqUf7I6")]
  [InlineData("https://youtube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://www.youtube.com/embed/b6-JNeXxN3s?si=9ON8XlL6BaqygVkW")]
  public void IsLinkVideoYouTube_ShouldNotAddNotification_WhenValueIsLinkVideoYouTube(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsLinkVideoYouTube(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("https://www.youtube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://youtu.be/b6-JNeXxN3s?si=Q3mm2FArjTqUf7I6")]
  [InlineData("https://youtube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://www.youtube.com/embed/b6-JNeXxN3s?si=9ON8XlL6BaqygVkW")]
  [InlineData("https://www.dailymotion.com/video/x9fbqre")]
  [InlineData("https://dai.ly/x9fbqre")]
  [InlineData("https://geo.dailymotion.com/player.html?video=x9fbqre")]
  [InlineData("https://www.y0utube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://vime0.com/24812648")]
  [InlineData("https://www.dailym0tion.com/video/x9fbqre")]
  [InlineData("")]
  [InlineData(null)]
  public void IsLinkVideoVimeo_ShouldAddNotification_WhenValueNotIsLinkVideoVimeo(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsLinkVideoVimeo(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("https://vimeo.com/24812648")]
  [InlineData("https://vimeo.com/24812648?share=copy")]
  [InlineData("https://player.vimeo.com/video/24812648?badge=0&autopause=0&player_id=0&app_id=58479")]
  public void IsLinkVideoVimeo_ShouldNotAddNotification_WhenValueIsLinkVideoVimeo(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsLinkVideoVimeo(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }

  // Teste para validar se o valor corresponde ao padrão e cria notificação, com valor que não corresponde
  [Theory]
  [InlineData("https://www.youtube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://youtu.be/b6-JNeXxN3s?si=Q3mm2FArjTqUf7I6")]
  [InlineData("https://youtube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://www.youtube.com/embed/b6-JNeXxN3s?si=9ON8XlL6BaqygVkW")]
  [InlineData("https://vimeo.com/24812648")]
  [InlineData("https://vimeo.com/24812648?share=copy")]
  [InlineData("https://player.vimeo.com/video/24812648?badge=0&autopause=0&player_id=0&app_id=58479")]
  [InlineData("https://www.y0utube.com/watch?v=b6-JNeXxN3s&list=RDMMb6-JNeXxN3s&start_radio=1")]
  [InlineData("https://vime0.com/24812648")]
  [InlineData("https://www.dailym0tion.com/video/x9fbqre")]
  [InlineData("")]
  [InlineData(null)]
  public void IsLinkVideoDailymotion_ShouldAddNotification_WhenValueNotIsLinkVideoDailymotion(string? valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam!;

    // Act
    validation.IsLinkVideoDailymotion(value, property);

    // Assert
    Assert.Single(validation.Notifications);
    Assert.Equal(property, validation.Notifications.First().Key);
  }

  // Teste para validar se o valor corresponde ao padrão e não cria notificação, com valor que corresponde
  [Theory]
  [InlineData("https://www.dailymotion.com/video/x9fbqre")]
  [InlineData("https://dai.ly/x9fbqre")]
  [InlineData("https://geo.dailymotion.com/player.html?video=x9fbqre")]
  public void IsLinkVideoDailymotion_ShouldNotAddNotification_WhenValueIsLinkVideoDailymotion(string valueParam)
  {
    // Arrange
    var property = "TestProperty";
    var validation = new Validation();
    string value = valueParam;

    // Act
    validation.IsLinkVideoDailymotion(value, property);

    // Assert
    Assert.Empty(validation.Notifications);
  }
}
