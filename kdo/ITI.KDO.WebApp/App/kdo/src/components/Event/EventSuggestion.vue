<template>
    <div id="app">
        <calendar
                :first-day="1"
                :all-events="events"
                :canAddEvent="true"
                :canDeleteEvent="true"
                @eventAdded="eventAdded"
                @eventDeleted="eventDeleted"
        ></calendar>
    </div>
</template>
 
<script>
    export default {
        name: 'app',
        data() {
            return {
                events: []
            }
        },
        components: {
            Calendar
        },
        methods: {
            eventAdded(event) {
                this.events.push(event);
            },
            eventDeleted(event) {
                this.events.splice(this.events.indexOf(event), 1);
            },
        },
        mounted() {
            let me = this;
            setTimeout(function () {
                me.events = [ // you can make ajax call here
                    {
                        id:1,
                        title:'Event 1',
                        description: 'Dummy Desc',
                        color: 'card-danger card-inverse',
                        date: new Date()
                    },
                ]
            }, 1000);
        }
    }
</script>