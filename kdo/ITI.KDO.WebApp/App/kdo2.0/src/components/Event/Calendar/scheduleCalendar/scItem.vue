<template>
    <div class="schedule-calendar-detail-item"
        :id="'exPopover-' + this.eId"
        @click="changeRouteEvent(item.eventId)">
        <b-popover
            :target = "'exPopover-' + this.eId"
            title="Event Information"
            triggers="hover focus">
            <div>
                <b-form-group label="Name" label-for="pop1" horizontal class="mb-1">
                    <b-form-input id="pop1" size="sm" :value="this.item.eventName" disabled/>
                </b-form-group>
                <b-form-group label="From" label-for="pop1" horizontal class="mb-1">
                    <b-form-input id="pop1" size="sm" :value="this.item.userId" disabled/>
                </b-form-group>
                <strong>Descriptions</strong>
                <b-alert show class="small">
                    {{ this.item.descriptions }}
                </b-alert>
            </div>
        </b-popover>
        <span class="schedule-calendar-detail-text" >{{ item.eventName }}</span>
    </div>
</template>

<script>
import EventApiService from '../../../../services/EventApiService';

export default {
    data() {
        return {
            eId: null,
        }
    },
    async mounted(){
        this.eId = this.item.eventId;
        console.log(this.eId);
    },
    props: {
        item: Object,
        date: Date,
        type: String,
    },
    methods: {
        onDrag(e) {
            this.$emit('item-dragstart', e, this.item, this.date, this.type);
        },
        async deleteEvent(){
            try {
                await this.executeAsyncRequest(() => EventApiService.deleteEventAsync(this.item.eventId));
            }
            catch(error) {
                alert("error")
            }
        },
        changeRouteEvent(eventId){
            this.$router.replace(`view/${eventId}`);
        }
        
    }
}
</script>
<style lang="less">
@import './variables.less';


.schedule-calendar-detail-item {
    padding: 0 5px;
    margin: 3px 6px 0;
    font-size: 12px;
    color: #fff;
    line-height: 2em;
    border-radius: 2px;
    background: @sc-primary-color;
    cursor: pointer;
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
    transition: .2s ease-in-out;

    &:hover {
        transform: translateY(-2px);
        box-shadow: 0 3px 8px rgba(0, 0, 0, .2), 0 -3px 8px rgba(0, 0, 0, .2)
    }
}
</style>
