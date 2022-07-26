import React from 'react';
import { confirmMessage, errorMessage, successMessage } from '../../../services/alerts';
import { Modal, Form, Input, Button } from "antd";
import { generalMessages } from '../../../constants/messages/general';
import InputRules from '../../../constants/inputRules';
import { inputValidationErrorMessages } from '../../../constants/messages/inputValidationErrors';
import { updateCategory } from '../../../services/categories';

function UpdateCategoryModal(props) {

    const close = () => {
        props.myClose();
    };

    const updateCategoryInfo = () => {
        props.updateCategoryInfo();
    }

    const onFinish = (values) => {
        confirmMessage()
            .then((result) => {
                if (result) {
                    if (props.title !== values.title) {

                        const model = {
                            currentTitle: props.title,
                            newTitle: values.title
                        };

                        updateCategory(model)
                            .then(() => {
                                close();
                                successMessage(
                                    generalMessages.CHANGE_DATA_SUCCESSFULLY
                                );
                                updateCategoryInfo();
                            });
                    } else {
                        errorMessage(
                            generalMessages.CHANGE_SOMETHING_TO_SAVE,
                            ""
                        );
                    }
                }
            });
    };

    const onFinishFailed = () => {
        errorMessage(
            generalMessages.CORRECT_ALL_COMMENTS,
            ""
        );
    };

    return (
        <Modal
            title="Edit category info"
            visible={true}
            onCancel={() => close()}
            cancelButtonProps={{ style: { display: 'none' } }}
            okButtonProps={{ style: { display: 'none' } }}
            footer={null}
        >
            <Form
                labelCol={{ span: 5 }}
                wrapperCol={{ span: 32 }}
                onFinish={onFinish}
                onFinishFailed={onFinishFailed}
                scrollToFirstError
            >
                <Form.Item
                    name="title"
                    label="Title: "
                    initialValue={props?.title}
                    rules={[
                        InputRules.required(
                            generalMessages.FIELD_MUST_NOT_BE_EMPTY
                        ),
                        InputRules.latinLetters(
                            inputValidationErrorMessages.NOT_VALID_CATEGORY
                        ),
                        InputRules.lengthRange(
                            2,
                            50,
                            inputValidationErrorMessages.CATEGORY_MUST_BE_BETWEEN_1_AND_50
                        )
                    ]}
                >
                    <Input />
                </Form.Item>

                <Form.Item>
                    <Button
                        block
                        htmlType="submit"
                        type="primary"
                        className="submitButton"
                    >
                        Save
                    </Button>
                </Form.Item>

                <Form.Item>
                    <Button
                        block
                        onClick={() => close()}
                    >
                        Close
                    </Button>
                </Form.Item>
            </Form>
        </Modal>
    );
};

export default UpdateCategoryModal;
