import{o as i,c as a,a as e,t as u,w as r,v as d,b as m}from"./index-a0e500f0.js";import{a as l}from"./axios-bff3f665.js";const g={key:0,class:"center-logout"},h={key:1,class:"center"},c=e("h1",null," Login ",-1),p={class:"amin"},_={class:"txt_field"},f=e("span",null,null,-1),v=e("label",{for:"username"},"Username",-1),y={class:"txt_field"},w=e("span",null,null,-1),I=e("label",{for:"password"},"Password",-1),k={class:"button-l"},L={key:0,class:"error-message"},$={data(){return{loginModel:{},isLoggedIn:!1,token:"",devUser:{},loginError:!1}},computed:{currentImage(){return this.images[this.currentIndex]}},methods:{prev(){this.currentIndex=(this.currentIndex-1+this.images.length)%this.images.length},next(){this.currentIndex=(this.currentIndex+1)%this.images.length},logout(){delete l.defaults.headers.common.Authorization,this.$store.commit("authenticate",null)},async login(){try{const o=(await l.post("https://localhost:5001/api/users/login",this.loginModel)).data;l.defaults.headers.common.Authorization=`Bearer ${o.token}`,this.$store.commit("authenticate",o),this.loginError=!1}catch(o){o.response.status==500&&(this.loginError=!0)}}},computed:{authenticated(){return this.$store.state.infos.isLoggedIn},username(){return this.$store.state.infos.name}}},x=Object.assign($,{__name:"Login",setup(o){return(t,s)=>(i(),a("main",null,[e("html",null,[e("body",null,[t.$store.state.infos.isLoggedIn?(i(),a("div",g,[e("h1",null,u(t.$store.state.infos.username),1),e("button",{onClick:s[0]||(s[0]=n=>t.logout())},"Logout ")])):(i(),a("div",h,[c,e("div",p,[e("div",_,[r(e("input",{type:"text",name:"username","onUpdate:modelValue":s[1]||(s[1]=n=>t.loginModel.username=n),id:"username"},null,512),[[d,t.loginModel.username]]),f,v]),e("div",y,[r(e("input",{id:"password",name:"password","onUpdate:modelValue":s[2]||(s[2]=n=>t.loginModel.password=n),type:"password"},null,512),[[d,t.loginModel.password]]),w,I]),e("div",k,[e("button",{onClick:s[3]||(s[3]=n=>t.login())},"Login"),t.loginError?(i(),a("p",L,"Login failed. Invalid credentials.")):m("",!0)])])]))])])]))}});export{x as default};