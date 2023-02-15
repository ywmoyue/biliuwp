using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliBili.UWP.Api.User
{
    public class LoginAPI
    {
        /// <summary>
        /// 二维码登录获取二维码及AuthCode
        /// </summary>
        /// <param name="mid"></param>
        /// <returns></returns>
        public ApiModel QRLoginAuthCode(string local_id)
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.POST,
                baseUrl = "https://passport.bilibili.com/x/passport-tv-login/qrcode/auth_code",
                body = ApiUtils.MustParameter(ApiUtils.AndroidTVKey, false)+ $"&local_id={local_id}",
            };
            api.body += ApiUtils.GetSign(api.body, ApiUtils.AndroidTVKey);
            return api;
        }

        /// <summary>
        /// 二维码登录轮询
        /// </summary>
        /// <param name="auth_code"></param>
        /// <returns></returns>
        public ApiModel QRLoginPoll(string auth_code, string local_id)
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.POST,
                baseUrl = "https://passport.bilibili.com/x/passport-tv-login/qrcode/poll",
                body = ApiUtils.MustParameter(ApiUtils.AndroidTVKey, false)+ $"&auth_code={auth_code}&guid={Guid.NewGuid().ToString()}&local_id={local_id}",
            };
            api.body += ApiUtils.GetSign(api.body, ApiUtils.AndroidTVKey);
            return api;
        }


        /// <summary>
        /// web版登录获取到的Cookie转app令牌
        /// </summary>
        /// <returns></returns>
        public ApiModel GetCookieToAccessKey()
        {
            var apiBody = "api=http://link.acg.tv/forum.php";
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.GET,
                baseUrl = "https://passport.bilibili.com/login/app/third",
                parameter = $"appkey={ApiUtils.AndroidKey.Appkey}&{apiBody}&sign=",
                need_cookie = true,
            };
            api.parameter += ApiHelper.GetSign(apiBody, ApiUtils.AndroidKey);
            return api;
        }

        /// <summary>
        /// web版登录获取到的Cookie转app令牌
        /// </summary>
        /// <returns></returns>
        public ApiModel GetCookieToAccessKey(string url)
        {
            ApiModel api = new ApiModel()
            {
                method = HttpMethod.GET,
                baseUrl = url,
                need_cookie = true,
                need_redirect = true,
            };
            return api;
        }
    }
}
