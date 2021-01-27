<template>
    <div class="schedule-calendar"style="margin-left: 21%;">
        <scHeader   :year="year"
                    :month="month"
                    @updateValue="updateView"></scHeader>
        <scBody     :year="year"
                    :month="month"
                    :startWeek="startWeek"
                    :direction="direction"
                    :data="keepData"></scBody>
    </div>
</template>
<script>
import { EventBus } from './utils';
import scHeader from './scHeader.vue';
import scBody from './scBody.vue';

export default {
    name: 'schedule-calendar',
    components: {
        scHeader,
        scBody
    },

    async mounted(){
        console.log(this.mode);
    },
    
    props: {
        startMonth: '',
        startWeek: {
            type: Number,
            default: 1
        },
        originData: {
            type: Array,
            default: []
        }
    },
    data() {
        return {
            year: new Date().getFullYear(),
            month: new Date().getMonth(),
            direction: 'Left',
            keepData: [...this.originData],
            calMode: this.mode,
            dragItem: null
        }
    },
    watch: {
        originData: function (data) {
            console.log(data);
            if (data.length) {
                this.keepData = [...data]
            }
        }
    },
    methods: {
        updateView({ year, month, direction }) {
            this.year = year
            this.month = month
            this.direction = direction
        },
        cellDragenter(e, date, type, index) {

        },
        itemDragstart(e, item, date, type) {
            this.dragItem = item
        },
        itemDrop(e, date, type, index) {
            if (!this.dragItem) return
            this.keepData.find(item => item.eventId === this.dragItem.eventId).dates = date
        }
    },
    created() {
        EventBus.$on('cell-dragenter', this.cellDragenter)
        EventBus.$on('item-dragstart', this.itemDragstart)
        EventBus.$on('item-drop', this.itemDrop)
    }
}
</script>
<style lang="less">
@import './variables.less';

.schedule- {
    &calendar {
        position: relative;
        display: flex;
        flex-direction: column;
        width: 1000px;
        height: 500px;
        color: @sc-base-color;
        font-size: @sc-base-font-size;
        box-shadow: @sc-box-shadow;

        *,
        *::before,
        *::after {
            box-sizing: border-box
        }

        button {
            border: 0;
            outline: none;
            cursor: pointer;
            background: transparent;
        }
    }
}
</style>
