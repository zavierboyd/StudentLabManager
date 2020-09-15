function createschema(classlist) {
    const schema = {
        "title": "Test Schedule",
        "type": "object",
        "properties": {
            "exam": {
                "required": true,
                "title": "Exam",
                "type": "string"
            },
            "group": {
                "required": true,
                "title": "Class",
                "type": "string",
                "enum": ["Class1", "Class2", "..."]
            },
            "duration": {
                "required": true,
                "title": "Duration in minutes",
                "type": "integer",
                "format": "number"
            },
            "schedule": {
                "required": true,
                "title": "Schedule",
                "type": "array",
                "format": "tabs",
                "items": {
                    "required": true,
                    "title": "Day",
                    "headerTemplate": "Day - {{i1}} | {{self.date}}",
                    "type": "object",
                    "properties": {
                        "date": {
                            "required": true,
                            "title": "Date",
                            "type": "string",
                            "format": "date"
                        },

                        "timeSlots": {
                            "required": true,
                            "type": "array",
                            "title": "Time Slots",
                            "format": "tabs-top",
                            "items": {
                                "required": true,
                                "title": "Time Slot",
                                "headerTemplate": "Slot - {{self.startTime}}",
                                "type": "object",
                                "properties": {
                                    "startTime": {
                                        "required": true,
                                        "title": "Start Time",
                                        "type": "string",
                                        "format": "time"
                                    },
                                    "studentNo": {
                                        "required": true,
                                        "type": "integer",
                                        "title": "Number of Students"
                                    },
                                    "place": {
                                        "type": "string",
                                        "required": true,
                                        "title": "Where is the exam?"
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    schema.properties.group.enum = classlist;
    return schema;
}