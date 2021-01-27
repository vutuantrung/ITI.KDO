<template>
    <div id="app"
         class="demo">
        <h1 class="demo-title">Calendar</h1>
        <div class="demo-container">
            <schedule-Calendar :originData="eventList"></schedule-Calendar>
        </div>
    </div>
</template>
<script>
import scheduleCalendar from './scheduleCalendar'
import EventApiService from '../../../services/EventApiService'
import UserApiService from '../../../services/UserApiService'
import AuthService from '../../../services/AuthService'

export default {
    name: 'app',
    components: {
        scheduleCalendar
    },

    data() {
        return {
            user: {},
            eventList: []
        }
    },

    async mounted() {
        var userEmail = AuthService.emailUser();
        this.user = await UserApiService.getUserAsync(userEmail);
        this.mode = this.$route.params.mode;
        await this.refreshList();

        if(this.mode == "suggest"){
            alert("Please choose your date.");
        }
    },
    
    methods: {
        async refreshList() {
            this.eventList = await EventApiService.getEventListAsync(this.user.userId);
            for (var i = 0; i < this.eventList.length; i++){
                this.eventList[i].dates = this.eventList[i].dates.slice(0, 10);
            }
        }
    }
}
</script>
<style lang="less">
html {
    background: #eee
}

body,
html {
    height: 100%;
    //overflow: hidden;
}

body {
    margin: 0;
}

.demo {
    margin-top: 120px;
    display: flex;
    flex-direction: column;
    height: 100%;
    padding: 0 30px 30px;
    box-sizing: border-box
}

.demo-title {
    text-align: center
}

.demo-container {
    flex: 1
}
</style>